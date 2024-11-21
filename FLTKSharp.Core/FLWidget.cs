using System.Drawing;
using FLTKSharp.Core.Interfaces;
using NLog;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLWidget
    : BaseFltkEventedObject
    , IFltkWidget
{
    public static IFltkWidget FromPointer(IntPtr pointer) => new FLWidget(pointer);
    public IntPtr GetPointer() => Pointer;
    private readonly Logger _log = LogManager.GetCurrentClassLogger();
    public FLWidget(int x, int y, int width, int height, string label = "")
        : this(Create(x, y, width, height, label, out var disposeAction))
    {
        _disposeActions.Add(disposeAction);
    }
    internal FLWidget(IntPtr pointer)
        : base(pointer)
    {
        _log.Properties["Pointer"] = pointer;
        _log.Trace($"Created " + pointer.ToString("x2"));
        base.FlObjectHandle = Fl_Widget_handle;
    }
    protected override FltkObjectHandleMethod FlObjectHandle
    {
        get => base.FlObjectHandle;
        set => base.FlObjectHandle = value;
    }

    private static IntPtr Create(int x, int y, int width, int height, string label, out Action disposeAction)
    {
        var labelPointer = InternalHelper.AllocateStringD(label, out disposeAction);
        return Fl_Widget_new(x, y, width, height, labelPointer);
    }
    #region IFltkWidget
    public Point Position
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_x)}(0x{Pointer:x2})");
            var x = Fl_Widget_x(Pointer);
            _log.Trace($"{nameof(Fl_Widget_y)}(0x{Pointer:x2})");
            var y = Fl_Widget_y(Pointer);
            return new(x, y);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            Resize(value, Size);
        }
    }

    public Size Size
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_width)}(0x{Pointer:x2})");
            var w = Fl_Widget_width(Pointer);
            _log.Trace($"{nameof(Fl_Widget_height)}(0x{Pointer:x2})");
            var h = Fl_Widget_height(Pointer);
            return new(w, h);
        }
        set
        {
            Resize(Position, value);
        }
    }

    public string? LabelText
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_label)}(0x{Pointer:x2})");
            var ptr = Fl_Widget_label(Pointer);
            return InternalHelper.ReadString(ptr);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            var ptr = InternalHelper.AllocateString(value, _disposeActions);
            _log.Trace($"{nameof(Fl_Widget_set_label)}(0x{Pointer:x2}, 0x{ptr:x2})");
            Fl_Widget_set_label(Pointer, ptr);
        }
    }

    public void Show()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Widget_show)}(0x{Pointer:x2})");
        Fl_Widget_show(Pointer);
    }

    public void Hide()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Widget_hide)}(0x{Pointer:x2})");
        Fl_Widget_hide(Pointer);
    }

    public bool Enabled
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_active)}(0x{Pointer:x2})");
            return Fl_Widget_active(Pointer) != 0;
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_active)}(0x{Pointer:x2})");
            if (Fl_Widget_active(Pointer) == 0)
            {
                _log.Trace($"{nameof(Fl_Widget_activate)}(0x{Pointer:x2})");
                Fl_Widget_activate(Pointer);
            }
            else
            {
                _log.Trace($"{nameof(Fl_Widget_deactivate)}(0x{Pointer:x2})");
                Fl_Widget_deactivate(Pointer);
            }
        }
    }

    public void Redraw()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Widget_redraw)}(0x{Pointer:x2})");
        Fl_Widget_redraw(Pointer);
    }
    public void RedrawLabel()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Widget_redraw_label)}(0x{Pointer:x2})");
        Fl_Widget_redraw_label(Pointer);
    }

    public void Resize(int x, int y, int width, int height)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Widget_resize)}(0x{Pointer:x2}, {x}, {y}, {width}, {height})");
        Fl_Widget_resize(Pointer, x, y, width, height);
    }
    public void Resize(Point position, Size size)
    {
        Resize(position.X, position.Y, size.Width, size.Height);
    }

    public string? TooltipText
    {
        get
        {

            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            var ptr = Fl_Widget_tooltip(Pointer);
            _log.Trace($"{nameof(Fl_Widget_tooltip)}(0x{Pointer:x2})");
            return InternalHelper.ReadString(ptr);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            var ptr = InternalHelper.AllocateString(value, _disposeActions);
            _log.Trace($"{nameof(Fl_Widget_set_tooltip)}(0x{Pointer:x2}, 0x{ptr:x2})");
            Fl_Widget_set_tooltip(Pointer, ptr);
        }
    }

    public int LabelFont
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_label_font)}(0x{Pointer:x2})");
            return Fl_Widget_label_font(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_set_label_font)}(0x{Pointer:x2}, {(int)value})");
            Fl_Widget_set_label_font(Pointer, value);
        }
    }

    public int LabelSize
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_label_size)}(0x{Pointer:x2})");
            return Fl_Widget_label_size(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_set_label_size)}(0x{Pointer:x2}, {(int)value})");
            Fl_Widget_set_label_size(Pointer, value);
        }
    }

    public FltkLabelType LabelType
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_label_type)}(0x{Pointer:x2})");
            var result = Fl_Widget_label_type(Pointer);
            return (FltkLabelType)result;
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }

            _log.Trace($"{nameof(Fl_Widget_set_label_type)}(0x{Pointer:x2}, {(int)value})");
            Fl_Widget_set_label_type(Pointer, (int)value);
        }
    }
    
    #endregion

    public uint LabelColor
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_label_color)}(0x{Pointer:x2})");
            return Fl_Widget_label_color(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_set_label_color)}(0x{Pointer:x2}, 0x{value:x2})");
            Fl_Widget_set_label_color(Pointer, value);
        }
    }

    public virtual void Focus()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Widget_take_focus)}(0x{Pointer:x2})");
        Fl_Widget_take_focus(Pointer);
    }

    public virtual bool IsActive(bool checkParents = true)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }

        if (checkParents)
        {
            _log.Trace($"{nameof(Fl_Widget_active_r)}(0x{Pointer:x2})");
            return Fl_Widget_active_r(Pointer) != 0;
        }
        else
        {
            _log.Trace($"{nameof(Fl_Widget_active)}(0x{Pointer:x2})");
            return Fl_Widget_active(Pointer) != 0;
        }
    }

    public virtual FltkAlign Alignment
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_align)}(0x{Pointer:x2})");
            var value = Fl_Widget_align(Pointer);
            return (FltkAlign)value;
        }
    }

    public virtual FltkBoxType BoxType
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_box)}(0x{Pointer:x2})");
            return (FltkBoxType)Fl_Widget_box(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Widget_set_box)}(0x{Pointer:x2}, {(int)value})");
            Fl_Widget_set_box(Pointer, (int)value);
        }
    }

    public virtual FLGroup AsGroup()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Widget_as_group)}(0x{Pointer:x2})");
        var ptr = Fl_Widget_as_group(Pointer);
        if (ptr == IntPtr.Zero)
        {
            throw new InvalidCastException($"Cannot cast into {nameof(FLGroup)} since {nameof(Fl_Widget_as_group)} returned a NULL pointer (for {Pointer})");
        }
        return new FLGroup(ptr);
    }

    public virtual FLWindow AsWindow()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Widget_as_window)}(0x{Pointer:x2})");
        var ptr = Fl_Widget_as_window(Pointer);
        if (ptr == IntPtr.Zero)
        {
            throw new InvalidCastException($"Cannot cast into {nameof(FLWindow)} since {nameof(Fl_Widget_as_window)} returned a NULL pointer (for {Pointer})");
        }
        return new FLWindow(ptr);
    }
}
