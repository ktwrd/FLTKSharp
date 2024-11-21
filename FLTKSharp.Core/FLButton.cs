using FLTKSharp.Core.Events;
using System.Drawing;
using NLog;
using static FLTKSharp.Core.CFltkNative;
using FLTKSharp.Core.Interfaces;
using System;

namespace FLTKSharp.Core;

public class FLButton
    : BaseFltkEventedObject
    , IFltkButton
{
    public static IFltkWidget FromPointer(IntPtr pointer) => new FLButton(pointer);
    public IntPtr GetPointer() => Pointer;
    private readonly Logger _log = LogManager.GetCurrentClassLogger();
    #region Initialize
    public FLButton(int width, int height, string text = "")
        : this(0, 0, width, height, text)
    { }
    public FLButton(int x, int y, int width, int height, string text = "")
        : this(Generate(x, y, width, height, text, out var dd))
    {
        _disposeActions.Add(dd);
    }
    internal FLButton(IntPtr ptr)
        : base(ptr)
    {
        _log.Properties["Pointer"] = ptr;
        _log.Trace($"Created {ptr:x2}");

        base.FlObjectHandle = Fl_Button_handle;
        UnsafeEvent += FLButton_UnsafeEvent;
        Disposed += FLButton_Disposed;
    }

    protected override FltkObjectHandleMethod FlObjectHandle
    {
        get => base.FlObjectHandle;
        set => base.FlObjectHandle = value;
    }

    private static IntPtr Generate(int x, int y, int width, int height, string text, out Action disposeAction)
    {
        var log = LogManager.GetLogger("FLButton.Generate");
        var ptr = InternalHelper.AllocateStringD(text, out disposeAction);
        log.Trace($"{nameof(Fl_Button_new)}({x}, {y}, {width}, {height}, {ptr:x2})");
        return Fl_Button_new(x, y, width, height, ptr);
    }
    #endregion

    #region Events
    private void FLButton_Disposed(object? sender, EventArgs e)
    {
        UnsafeEvent -= FLButton_UnsafeEvent;
    }

    private void FLButton_UnsafeEvent(object? sender, FltkUnsafeEventArgs e)
    {
        if (e.Widget != Pointer)
            return;
        switch (e.Event)
        {
            case FltkEvent.Push:
                MouseDown?.Invoke(this, new FltkMouseEventArgs());
                break;
            case FltkEvent.Released:
            MouseUp?.Invoke(this, new FltkMouseEventArgs());
                break;
        }
    }

    public event EventHandler<FltkMouseEventArgs>? MouseDown;
    public event EventHandler<FltkMouseEventArgs>? MouseUp;
    #endregion

    #region IFltkButton
    public int Shortcut
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            return Fl_Button_shortcut(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            Fl_Button_set_shortcut(Pointer, value);
        }
    }

    public int Clear()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        return Fl_Button_clear(Pointer);
    }

    public int Value
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            return Fl_Button_value(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            Fl_Button_set_value(Pointer, value);
        }
    }

    public FltkBoxType PressBoxType
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace(nameof(Fl_Button_down_box));
            var value = Fl_Button_down_box(Pointer);
            return (FltkBoxType)value;
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace(nameof(Fl_Button_set_down_box));
            Fl_Button_set_down_box(Pointer, (int)value);
        }
    }

    public bool Compact
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace(nameof(Fl_Button_compact));
            var value = Fl_Button_compact(Pointer);
            return value == '1';
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace(nameof(Fl_Button_set_compact));
            Fl_Button_set_compact(Pointer, value ? '1' : '0');
        }
    }

    public Color BackgroundColor
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Button_color)}(0x{Pointer:x2})");
            var value = Fl_Button_color(Pointer);
            uint r = 0;
            uint g = 0;
            uint b = 0;
            _log.Trace($"{nameof(Fl_get_color_rgb)}(0x{value:x2}, uint*, uint*, uint*)");
            Fl_get_color_rgb(value, ref r, ref g, ref b);
            return Color.FromArgb(255, (byte)r, (byte)g, (byte)g);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }

            _log.Trace($"{nameof(Fl_rgb_color)}(0x{value.R:x2}, 0x{value.G:x2}, 0x{value.B:x2})");
            var num = Fl_rgb_color((char)value.R, (char)value.G, (char)value.B);

            _log.Trace($"{nameof(Fl_Button_set_color)}(0x{Pointer:x2}, 0x{num:x2})");
            Fl_Button_set_color(Pointer, num);
        }
    }
    #endregion

    #region IFltkWidget
    public Point Position
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Button_x)}(0x{Pointer:x2})");
            var x = Fl_Button_x(Pointer);
            _log.Trace($"{nameof(Fl_Button_y)}(0x{Pointer:x2})");
            var y = Fl_Button_y(Pointer);
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
            _log.Trace($"{nameof(Fl_Button_width)}(0x{Pointer:x2})");
            var w = Fl_Button_width(Pointer);
            _log.Trace($"{nameof(Fl_Button_height)}(0x{Pointer:x2})");
            var h = Fl_Button_height(Pointer);
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
            _log.Trace($"{nameof(Fl_Button_label)}(0x{Pointer:x2})");
            var ptr = Fl_Button_label(Pointer);
            return InternalHelper.ReadString(ptr);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            var ptr = InternalHelper.AllocateString(value, _disposeActions);
            _log.Trace($"{nameof(Fl_Button_set_label)}(0x{Pointer:x2}, 0x{ptr:x2})");
            Fl_Button_set_label(Pointer, ptr);
        }
    }

    public void Show()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Button_show)}(0x{Pointer:x2})");
        Fl_Button_show(Pointer);
    }

    public void Hide()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Button_hide)}(0x{Pointer:x2})");
        Fl_Button_hide(Pointer);
    }

    public bool Enabled
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Button_active)}(0x{Pointer:x2})");
            return Fl_Button_active(Pointer) != 0;
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Button_active)}(0x{Pointer:x2})");
            if (Fl_Button_active(Pointer) == 0)
            {
                _log.Trace($"{nameof(Fl_Button_activate)}(0x{Pointer:x2})");
                Fl_Button_activate(Pointer);
            }
            else
            {
                _log.Trace($"{nameof(Fl_Button_deactivate)}(0x{Pointer:x2})");
                Fl_Button_deactivate(Pointer);
            }
        }
    }

    public void Redraw()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Button_redraw)}(0x{Pointer:x2})");
        Fl_Button_redraw(Pointer);
    }
    public void RedrawLabel()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Button_redraw_label)}(0x{Pointer:x2})");
        Fl_Button_redraw_label(Pointer);
    }

    public void Resize(int x, int y, int width, int height)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Button_resize)}(0x{Pointer:x2}, {x}, {y}, {width}, {height})");
        Fl_Button_resize(Pointer, x, y, width, height);
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
            var ptr = Fl_Button_tooltip(Pointer);
            _log.Trace($"{nameof(Fl_Button_tooltip)}(0x{Pointer:x2})");
            return InternalHelper.ReadString(ptr);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            var ptr = InternalHelper.AllocateString(value, _disposeActions);
            _log.Trace($"{nameof(Fl_Button_set_tooltip)}(0x{Pointer:x2}, 0x{ptr:x2})");
            Fl_Button_set_tooltip(Pointer, ptr);
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
            _log.Trace($"{nameof(Fl_Button_label_font)}(0x{Pointer:x2})");
            return Fl_Button_label_font(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Button_set_label_font)}(0x{Pointer:x2}, {(int)value})");
            Fl_Button_set_label_font(Pointer, value);
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
            _log.Trace($"{nameof(Fl_Button_label_size)}(0x{Pointer:x2})");
            return Fl_Button_label_size(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Button_set_label_size)}(0x{Pointer:x2}, {(int)value})");
            Fl_Button_set_label_size(Pointer, value);
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
            _log.Trace($"{nameof(Fl_Button_label_type)}(0x{Pointer:x2})");
            var result = Fl_Button_label_type(Pointer);
            return (FltkLabelType)result;
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }

            _log.Trace($"{nameof(Fl_Button_set_label_type)}(0x{Pointer:x2}, {(int)value})");
            Fl_Button_set_label_type(Pointer, (int)value);
        }
    }
    #endregion
}
