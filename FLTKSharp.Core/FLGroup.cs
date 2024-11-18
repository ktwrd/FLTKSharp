using System.Runtime.InteropServices;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLGroup : FLWidget
{
    public FLGroup(int x, int y, int width, int height, string? label = null)
        : base(Create(x, y, width, height, label, out var disposeAction))
    {
        _disposeActions.Add(disposeAction);
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
}
