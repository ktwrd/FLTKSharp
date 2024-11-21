using FLTKSharp.Core.Events;
using FLTKSharp.Core.Interfaces;
using NLog;
using System.Drawing;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLWindow
    : BaseFltkEventedObject
    , IFltkWindow
{
    public IntPtr GetPointer() => Pointer;
    #region Initialize
    private readonly Logger _log = LogManager.GetCurrentClassLogger();
    public static IFltkWidget FromPointer(IntPtr pointer)
    {
        return new FLWidget(pointer).AsWindow();
    }
    public FLWindow(int width, int height, string title = "")
        : this(Create(width, height, title, out var dd))
    {
        _disposeActions.Add(dd);
    }

    public FLWindow(int x, int y, int width, int height, string title = "")
        : this(Create(x, y, width, height, title, out var dd))
    {
        _disposeActions.Add(dd);
    }

    internal FLWindow(IntPtr pointer)
        : base(pointer)
    {
        _log.Properties["Pointer"] = pointer;
        UnsafeEvent += FLWindow_UnsafeEvent;
        base.FlObjectHandle = (a, b, c) =>
        {
            _log.Trace($"{nameof(Fl_Window_handle)}(0x{a:x2}, 0x{b:x2}, 0x{c:x2})");
            Fl_Window_handle(a, b, c);
        };
    }

    protected override FltkObjectHandleMethod FlObjectHandle
    {
        get => base.FlObjectHandle;
        set => base.FlObjectHandle = value;
    }
    
    private static IntPtr Create(int x, int y, int width, int height, string title, out Action disposeAction)
    {
        var log = LogManager.GetLogger("FLWindow.Create");
        var ptr = InternalHelper.AllocateStringD(title, out disposeAction);
        log.Trace($"{nameof(Fl_Window_new)}({x}, {y}, {width}, {height}, 0x{ptr:x2})");
        return Fl_Window_new(x, y, width, height, ptr);
    }

    private static IntPtr Create(int width, int height, string title, out Action disposeAction)
    {
        var log = LogManager.GetLogger("FLWindow.Create");
        var ptr = InternalHelper.AllocateStringD(title, out disposeAction);
        log.Trace($"{nameof(Fl_Window_new_wh)}({width}, {height}, 0x{ptr:x2})");
        return Fl_Window_new_wh(width, height, ptr);
    }
    #endregion


    #region Events
    private void FLWindow_UnsafeEvent(object? sender, FltkUnsafeEventArgs e)
    {
        _log.Debug($"Event: {e.Event}");
        if (e.Widget != Pointer)
            return;
        switch (e.Event)
        {
            case FltkEvent.Show:
                Shown?.Invoke(this, EventArgs.Empty);
                break;
        }
    }
    public event EventHandler? Shown;
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
            _log.Trace($"{nameof(Fl_Window_x)}(0x{Pointer:x2})");
            var x = Fl_Window_x(Pointer);
            _log.Trace($"{nameof(Fl_Window_y)}(0x{Pointer:x2})");
            var y = Fl_Window_y(Pointer);
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
            _log.Trace($"{nameof(Fl_Window_width)}(0x{Pointer:x2})");
            var w = Fl_Window_width(Pointer);
            _log.Trace($"{nameof(Fl_Window_height)}(0x{Pointer:x2})");
            var h = Fl_Window_height(Pointer);
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
            _log.Trace($"{nameof(Fl_Window_label)}(0x{Pointer:x2})");
            var ptr = Fl_Window_label(Pointer);
            return InternalHelper.ReadString(ptr);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            var ptr = InternalHelper.AllocateString(value, _disposeActions);
            _log.Trace($"{nameof(Fl_Window_set_label)}(0x{Pointer:x2}, 0x{ptr:x2})");
            Fl_Window_set_label(Pointer, ptr);
        }
    }

    public void Show()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_show)}(0x{Pointer:x2})");
        Fl_Window_show(Pointer);
    }

    public void Hide()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_hide)}(0x{Pointer:x2})");
        Fl_Window_hide(Pointer);
    }

    public bool Enabled
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Window_active)}(0x{Pointer:x2})");
            return Fl_Window_active(Pointer) != 0;
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Window_active)}(0x{Pointer:x2})");
            if (Fl_Window_active(Pointer) == 0)
            {
                _log.Trace($"{nameof(Fl_Window_activate)}(0x{Pointer:x2})");
                Fl_Window_activate(Pointer);
            }
            else
            {
                _log.Trace($"{nameof(Fl_Window_deactivate)}(0x{Pointer:x2})");
                Fl_Window_deactivate(Pointer);
            }
        }
    }

    public void Redraw()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_redraw)}(0x{Pointer:x2})");
        Fl_Window_redraw(Pointer);
    }
    public void RedrawLabel()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_redraw_label)}(0x{Pointer:x2})");
        Fl_Window_redraw_label(Pointer);
    }

    public void Resize(int x, int y, int width, int height)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_resize)}(0x{Pointer:x2}, {x}, {y}, {width}, {height})");
        Fl_Window_resize(Pointer, x, y, width, height);
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
            var ptr = Fl_Window_tooltip(Pointer);
            _log.Trace($"{nameof(Fl_Window_tooltip)}(0x{Pointer:x2})");
            return InternalHelper.ReadString(ptr);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            var ptr = InternalHelper.AllocateString(value, _disposeActions);
            _log.Trace($"{nameof(Fl_Window_set_tooltip)}(0x{Pointer:x2}, 0x{ptr:x2})");
            Fl_Window_set_tooltip(Pointer, ptr);
        }
    }

    public int Type
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            return Fl_Window_get_type(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            Fl_Window_set_type(Pointer, value);
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
            _log.Trace($"{nameof(Fl_Window_label_font)}(0x{Pointer:x2})");
            return Fl_Window_label_font(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Window_set_label_font)}(0x{Pointer:x2}, {(int)value})");
            Fl_Window_set_label_font(Pointer, value);
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
            _log.Trace($"{nameof(Fl_Window_label_size)}(0x{Pointer:x2})");
            return Fl_Window_label_size(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Window_set_label_size)}(0x{Pointer:x2}, {(int)value})");
            Fl_Window_set_label_size(Pointer, value);
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
            _log.Trace($"{nameof(Fl_Window_label_type)}(0x{Pointer:x2})");
            var result = Fl_Window_label_type(Pointer);
            return (FltkLabelType)result;
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }

            _log.Trace($"{nameof(Fl_Window_set_label_type)}(0x{Pointer:x2}, {(int)value})");
            Fl_Window_set_label_type(Pointer, (int)value);
        }
    }

    #endregion

    #region IFltkGroup
    public void Begin()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_begin)}(0x{Pointer:x2})");
        Fl_Window_begin(Pointer);
    }
    public void End()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_end)}(0x{Pointer:x2})");
        Fl_Window_end(Pointer);
    }

    public int IndexOfChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_children)}(0x{Pointer:x2})");
        var count = Fl_Window_children(Pointer);
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Window_find)}(0x{Pointer:x2}, 0x{ptr:x2})");
        var result = Fl_Window_find(Pointer, ptr);
        if (result >= count)
        {
            return -1;
        }
        return result;
    }

    public void AddChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Window_add)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Window_add(Pointer, ptr);
    }
    public void InsertChildAt(IFltkWidget child, int index)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Window_insert)}(0x{Pointer:x2}, 0x{ptr:x2}, {index})");
        Fl_Window_insert(Pointer, ptr, index);
    }
    public void RemoveChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Window_remove)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Window_remove(Pointer, ptr);
    }
    public void RemoveChild(int index)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_remove_by_index)}(0x{Pointer:x2}, {index})");
        Fl_Window_remove_by_index(Pointer, index);
    }
    public void RemoveAllChildren()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_clear)}(0x{Pointer:x2})");
        Fl_Window_clear(Pointer);
    }

    public int ChildCount()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_children)}(0x{Pointer:x2})");
        return Fl_Window_children(Pointer);
    }
    public object? ChildAt<T>(int index) where T : IFltkWidget
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_children)}(0x{Pointer:x2})");
        var count = Fl_Window_children(Pointer);
        _log.Trace($"{nameof(Fl_Window_child)}(0x{Pointer:x2}, {index})");
        var childPtr = Fl_Window_child(Pointer, index);
        if (childPtr == IntPtr.Zero)
            return null;
        return T.FromPointer(childPtr);
    }

    public bool ClipChildren
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            _log.Trace($"{nameof(Fl_Window_clip_children)}(0x{Pointer:x2})");
            return Fl_Window_clip_children(Pointer) != 0;
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            var v = value ? 1 : 0;
            _log.Trace($"{nameof(Fl_Window_set_clip_children)}(0x{Pointer:x2}, {v})");
            Fl_Window_set_clip_children(Pointer, v);
        }
    }

    public void InitializeSizes()
    {

        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_init_sizes)}(0x{Pointer:x2})");
        Fl_Window_init_sizes(Pointer);
    }
    public void DrawChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Window_draw_child)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Window_draw_child(Pointer, ptr);
    }

    public void UpdateChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Window_update_child)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Window_update_child(Pointer, ptr);
    }

    public void DrawOutsideLabel(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Window_draw_outside_label)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Window_draw_outside_label(Pointer, ptr);
    }

    public void DrawChildren()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
        }
        _log.Trace($"{nameof(Fl_Window_draw_children)}(0x{Pointer:x2})");
        Fl_Window_draw_children(Pointer);
    }
    #endregion

    public bool Visible
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer");
            }
            return Fl_Window_shown(Pointer) != 0;
        }
    }
}
