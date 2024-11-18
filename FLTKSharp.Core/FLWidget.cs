using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLWidget : BaseFltkObject
{
    public FLWidget(int x, int y, int width, int height, string label)
        : base(Create(x, y, width, height, label, out var disposeAction))
    {
        _disposeActions.Add(disposeAction);
    }
    protected FLWidget(IntPtr pointer)
        : base(pointer)
    { }

    private static IntPtr Create(int x, int y, int width, int height, string label, out Action disposeAction)
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
        return Fl_Widget_new(x, y, width, height, labelPointer);
    }

    public void Show()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Widget_show(Pointer);
    }
    public void Hide()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Widget_hide(Pointer);
    }

    public virtual string? Label
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            var ptr = Fl_Widget_label(Pointer);
            return Marshal.PtrToStringAnsi(ptr);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            var ptr = Marshal.StringToHGlobalAnsi(value);
            _disposeActions.Add(() => Marshal.FreeHGlobal(ptr));
            Fl_Widget_set_label(Pointer, ptr);
        }
    }

    public virtual int LabelSize
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            return Fl_Widget_label_size(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            Fl_Widget_set_label_size(Pointer, value);
        }
    }

    public virtual FltkLabelType LabelType
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            var result = Fl_Widget_label_type(Pointer);
            return (FltkLabelType)result;
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            Fl_Widget_set_label_type(Pointer, (int)value);
        }
    }
}
