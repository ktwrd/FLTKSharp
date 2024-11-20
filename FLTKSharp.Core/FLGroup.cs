using System.Runtime.InteropServices;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLGroup : FLWidget
{
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
        base.FlObjectHandle = Fl_Group_handle;
    }

    protected override FltkObjectHandleMethod FlObjectHandle
    {
        get => base.FlObjectHandle;
        set => base.FlObjectHandle = value;
    }

    private static IntPtr Create(int x, int y, int width, int height, string? label, out Action disposeAction)
    {
        IntPtr labelPointer = IntPtr.Zero;
        if (string.IsNullOrEmpty(label))
        {
            disposeAction = () => { };
        }
        else
        {
            labelPointer = Marshal.StringToHGlobalAnsi(label);
            disposeAction = () => Marshal.FreeHGlobal(labelPointer);
        }
        return Fl_Group_new(x, y, width, height, labelPointer);
    }
    public void AddChild(FLWidget widget)
    {
        if (widget.Pointer == IntPtr.Zero)
        {
            throw new ArgumentException($"Null pointer", nameof(widget));
        }
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Group_add(Pointer, widget.Pointer);
    }
    public void RemoveChild(FLWidget widget)
    {
        if (widget.Pointer == IntPtr.Zero)
        {
            throw new ArgumentException($"Null pointer", nameof(widget));
        }
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Group_remove(Pointer, widget.Pointer);
    }
    public void RemoveAt(int index)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Group_remove_by_index(Pointer, index);
    }

    public virtual void Begin()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Group_begin(Pointer);
    }
    public virtual void End()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Group_end(Pointer);
    }
}
