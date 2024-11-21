using System.Runtime.InteropServices;
using NLog;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLGroup : FLWidget
{
    private readonly Logger _log = LogManager.GetCurrentClassLogger();
    protected override void Disposing(bool disposed)
    {
        base.Disposing(disposed);
        var current = Fl_Group_current();
        if (current == Pointer)
            Fl_Group_end(Pointer);
        Fl_Group_clear(Pointer);
    }

    public FLGroup(int x, int y, int width, int height, string? label = null)
        : base(Create(x, y, width, height, label, out var disposeAction))
    {
        _disposeActions.Add(disposeAction);
    }

    internal FLGroup(IntPtr pointer)
        : base(pointer)
    {
        _log.Properties["Pointer"] = pointer;
        base.FlObjectHandle = Fl_Group_handle;
    }

    protected override FltkObjectHandleMethod FlObjectHandle
    {
        get => base.FlObjectHandle;
        set => base.FlObjectHandle = value;
    }

    private static IntPtr Create(int x, int y, int width, int height, string? label, out Action disposeAction)
    {
        IntPtr labelPointer = InternalHelper.AllocateStringD(label, out disposeAction);
        return Fl_Group_new(x, y, width, height, labelPointer);
    }
    public virtual void AddChild(FLWidget widget)
    {
        if (widget.Pointer == IntPtr.Zero)
        {
            throw new ArgumentException($"Null pointer", nameof(widget));
        }
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace(nameof(Fl_Group_add));
        Fl_Group_add(Pointer, widget.Pointer);
    }
    public virtual void RemoveChild(FLWidget widget)
    {
        if (widget.Pointer == IntPtr.Zero)
        {
            throw new ArgumentException($"Null pointer", nameof(widget));
        }
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace(nameof(Fl_Group_remove));
        Fl_Group_remove(Pointer, widget.Pointer);
    }
    public void RemoveAt(int index)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace(nameof(Fl_Group_remove_by_index));
        Fl_Group_remove_by_index(Pointer, index);
    }

    public virtual void Begin()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace(nameof(Fl_Group_begin));
        Fl_Group_begin(Pointer);
    }
    public virtual void End()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace(nameof(Fl_Group_end));
        Fl_Group_end(Pointer);
    }
}
