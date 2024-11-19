using NLog;

namespace FLTKSharp.Core;

/// <summary>
/// Base object for things in FLTK that are defined by a pointer and have things that need to be disposed at some point.
/// </summary>
public class BaseFltkObject : IDisposable
{
    private readonly Logger _log;
    private bool _hasDisposed = false;
    public void Dispose()
    {
        var exceptionList = new List<Exception>();
        try
        {
            Disposing(_hasDisposed);
        }
        catch (Exception ex)
        {
            _log.Error($"Failed to call {nameof(Disposing)}({_hasDisposed})\n{ex}");
        }
        _hasDisposed = true;
        foreach (var item in _disposeActions)
        {
            try
            {
                item();
            }
            catch (Exception ex)
            {
                exceptionList.Add(ex);
            }
        }
        if (exceptionList.Count > 0)
        {
            _log.Error($"Failed to dispose one or more objects\n" + string.Join("\n", exceptionList.Select(v => v.ToString())));
        }
        OnDisposed();
    }
    protected virtual void Disposing(bool disposed)
    { }
    protected List<Action> _disposeActions = [];
    private readonly int _createdOnManagedThreadId;
    public BaseFltkObject(IntPtr pointer)
    {
        _log = LogManager.GetCurrentClassLogger();
        _log.Properties["Pointer"] = pointer.ToString("16");
        _createdOnManagedThreadId = Thread.CurrentThread.ManagedThreadId;
        Pointer = pointer;
    }
    internal IntPtr Pointer { get; private set; }


    private List<EventHandler> disposedEventTargets = [];

    /// <summary>
    /// Invoked when this object has been disposed.
    /// </summary>
    public event EventHandler Disposed
    {
        add
        {
            lock (disposedEventTargets)
            {
                disposedEventTargets.Add(value);
            }
        }
        remove
        {
            lock (disposedEventTargets)
            {
                disposedEventTargets.Remove(value);
            }
        }
    }

    private void OnDisposed()
    {
        var errors = new Dictionary<int, Exception>();
        for (int i = 0; i < disposedEventTargets.Count; i++)
        {
            try
            {
                disposedEventTargets[i]?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                errors[i] = ex;
            }
        }
        if (errors.Count > 0)
        {
            var items = errors.Select(v => String.Format("{0}: {1}", v.Key.ToString().PadLeft(4, ' '), v.Value.ToString()));
            _log.Error($"Failed to invoke {errors.Count} events\n" + string.Join(Environment.NewLine, items));
        }
    }
}
