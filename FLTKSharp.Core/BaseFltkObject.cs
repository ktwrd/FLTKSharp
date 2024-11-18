namespace FLTKSharp.Core;

/// <summary>
/// Base object for things in FLTK that are defined by a pointer and have things that need to be disposed at some point.
/// </summary>
public class BaseFltkObject : IDisposable
{
    public void Dispose()
    {
        var exceptionList = new List<Exception>();
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
            throw new AggregateException("Failed to dispose one or more objects", exceptionList);
        }
    }
    protected List<Action> _disposeActions = [];
    internal BaseFltkObject(IntPtr pointer)
    {
        Pointer = pointer;
    }
    public IntPtr Pointer { get; protected set; }
}
