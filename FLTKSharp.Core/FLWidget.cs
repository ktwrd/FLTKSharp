using System.Runtime.InteropServices;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLWidget : BaseFltkObject
{
    public FLWidget(int x, int y, int width, int height, string label)
        : this(Create(x, y, width, height, label, out var disposeAction))
    {
        _disposeActions.Add(disposeAction);
    }
    protected FLWidget(IntPtr pointer)
        : base(pointer)
    {
        FlObjectHandle = Fl_Widget_handle;
    }
    protected override FltkObjectHandleMethod FlObjectHandle
    {
        get => base.FlObjectHandle;
        set => base.FlObjectHandle = value;
    }

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

    public virtual void Show()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Widget_show(Pointer);
    }
    public virtual void Hide()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Widget_hide(Pointer);
    }

    public virtual void Focus()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Widget_take_focus(Pointer);
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
            var ptr = InternalHelper.AllocateString(value, _disposeActions);
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

    public virtual bool IsActive(bool checkParents = true)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }

        var value = 0;
        if (checkParents)
        {
            value = Fl_Widget_active_r(Pointer);
        }
        else
        {
            value = Fl_Widget_active(Pointer);
        }
        return value != 0;
    }

    public virtual void Activate()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Widget_activate(Pointer);
    }

    public virtual FltkAlign Alignment
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            var value = Fl_Widget_align(Pointer);
            return (FltkAlign)value;
        }
    }

    public FLGroup AsGroup()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        var ptr = Fl_Widget_as_group(Pointer);
        if (ptr == IntPtr.Zero)
        {
            throw new InvalidCastException($"Cannot cast into {nameof(FLGroup)} since {nameof(Fl_Widget_as_group)} returned a NULL pointer (for {Pointer})");
        }
        return new FLGroup(ptr);
    }

    public FLWindow AsWindow()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        var ptr = Fl_Widget_as_window(Pointer);
        if (ptr == IntPtr.Zero)
        {
            throw new InvalidCastException($"Cannot cast into {nameof(FLWindow)} since {nameof(Fl_Widget_as_window)} returned a NULL pointer (for {Pointer})");
        }
        return new FLWindow(ptr);
    }
}
