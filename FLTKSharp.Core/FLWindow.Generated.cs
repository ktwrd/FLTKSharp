using FLTKSharp.Core.Events;
using FLTKSharp.Core.Interfaces;
using NLog;
using System.Drawing;
using static FLTKSharp.Core.CFltkNative;


namespace FLTKSharp.Core;

#nullable enable
public class FLWindow : FLTKSharp.Core.BaseFltkEventedObject, FLTKSharp.Core.Interfaces.IFltkWidget, FLTKSharp.Core.Interfaces.IFltkGroup, FLTKSharp.Core.Interfaces.IFltkWindow
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
    
    private void FLWindow_UnsafeEvent(object? sender, FltkUnsafeEventArgs e)
    {
        _log.Debug($"Event: {e.Event}");
        if (e.Widget != Pointer)
            return;
    }
    #endregion

    #region CodeTemplate.IFltkWidget.txt
    public Point Position
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_x)}(0x{Pointer:x2})");
    		var x = Fl_Window_x(Pointer);
    		_log.Trace($"{nameof(Fl_Window_x)}(0x{Pointer:x2})");
    		var y = Fl_Window_y(Pointer);
    		return new Point(x, y);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		Resize(value, Size);
    	}
    }
    public Size Size
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_width)}(0x{Pointer:x2})");
    		var w = Fl_Window_width(Pointer);
    		_log.Trace($"{nameof(Fl_Window_height)}(0x{Pointer:x2})");
    		var h = Fl_Window_height(Pointer);
    		return new Size(w, h);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		Resize(Position, value);
    	}
    }
    
    public string? LabelText
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_label)}(0x{Pointer:x2})");
    		var ptr = Fl_Window_label(Pointer);
    		return InternalHelper.ReadString(ptr);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		var ptr = InternalHelper.AllocateString(value, _disposeActions);
    		_log.Trace($"{nameof(Fl_Window_set_label)}(0x{Pointer:x2}, 0x{ptr:x2})");
    		Fl_Window_set_label(Pointer, ptr);
    	}
    }
    
    public void Redraw()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Window_redraw)}(0x{Pointer:x2})");
    	Fl_Window_redraw(Pointer);
    }
    
    public void Show()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Window_show)}(0x{Pointer:x2})");
    	Fl_Window_show(Pointer);
    }
    
    public void Hide()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Window_hide)}(0x{Pointer:x2})");
    	Fl_Window_hide(Pointer);
    }
    
    public bool Enabled
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_active)}(0x{Pointer:x2})");
    		return Fl_Window_active(Pointer) != 0;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
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
    
    public void Resize()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Window_redraw)}(0x{Pointer:x2})");
    	Fl_Window_redraw(Pointer);
    }
    
    public void RedrawLabel()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Window_redraw_label)}(0x{Pointer:x2})");
    	Fl_Window_redraw_label(Pointer);
    }
    
    public void Resize(int x, int y, int width, int height)
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
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
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_tooltip)}(0x{Pointer:x2})");
    		var ptr = Fl_Window_tooltip(Pointer);
    		return InternalHelper.ReadString(ptr);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		var ptr = InternalHelper.AllocateString(value, _disposeActions);
    		_log.Trace($"{nameof(Fl_Window_set_tooltip)}(0x{Pointer:x2}, 0x{ptr:x2})");
    		Fl_Window_set_tooltip(Pointer, ptr);
    	}
    }
    
    public int LabelFont
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_label_font)}(0x{Pointer:x2})");
    		return Fl_Window_label_font(Pointer);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_set_label_font)}(0x{Pointer:x2}, 0x{value:0x})");
    		Fl_Window_set_label_font(Pointer, value);
    	}
    }
    
    public int LabelSize
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_label_size)}(0x{Pointer:x2})");
    		return Fl_Window_label_size(Pointer);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_set_label_size)}(0x{Pointer:x2}, {value})");
    		Fl_Window_set_label_size(Pointer, value);
    	}
    }
    
    public FltkLabelType LabelType
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_label_type)}(0x{Pointer:x2})");
    		var result = Fl_Window_label_type(Pointer);
    		return (FltkLabelType)result;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_set_label_type)}(0x{Pointer:x2}, {(int)value})");
    		Fl_Window_set_label_type(Pointer, (int)value);
    	}
    }
    
    public Color LabelColor
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		var c = Fl_Window_label_color(Pointer);
    		uint r = 0;
    		uint g = 0;
    		uint b = 0;
    		Fl_get_color_rgb(c, ref r, ref g, ref b);
    		return Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_rgb_color)}(0x{value.R:x2}, 0x{value.G:x2}, 0x{value.B:x2})");
    		var c = Fl_rgb_color((char)value.R, (char)value.G, (char)value.B);
    
    		_log.Trace($"{nameof(Fl_Window_set_label_color)}(0x{Pointer:x2}, 0x{c:x2})");
    		Fl_Window_set_label_color(Pointer, c);
    	}
    }
    
    public Size LabelDimensions
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		int w = 0;
    		int h = 0;
    		_log.Trace($"{nameof(Fl_Window_measure_label)}(0x{Pointer:x2})");
    		Fl_Window_measure_label(Pointer, ref w, ref h);
    		_log.Trace($"{nameof(Fl_Window_measure_label)}(0x{Pointer:x2}) -> w: {w}, h: {h}");
    
    		return new Size(w, h);
    	}
    }
    
    public FltkBoxType BoxType
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_box)}(0x{Pointer:x2})");
    		var value = Fl_Window_box(Pointer);
    		return (FltkBoxType)value;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_set_box)}(0x{Pointer:x2}, 0x{(int)value:x2})");
    		Fl_Window_set_box(Pointer, (int)value);
    	}
    }
    
    
    public FltkAlign Alignment
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_align)}(0x{Pointer:x2})");
    		var value = Fl_Window_align(Pointer);
    		var u = Convert.ToUInt16(value);
    		return (FltkAlign)u;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_set_align)}(0x{Pointer:x2}, 0x{(int)value:x2})");
    		Fl_Window_set_align(Pointer, (int)value);
    	}
    }
    
    public void Delete()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Window_delete)}(0x{Pointer:x2})");
    	Fl_Window_delete(Pointer);
    }
    
    public Color SelectionColor
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Window_selection_color)}(0x{Pointer:x2})");
    		var value = Fl_Window_selection_color(Pointer);
    		uint r = 0;
    		uint g = 0;
    		uint b = 0;
    		_log.Trace($"{nameof(Fl_get_color_rgb)}(0x{value:x2})");
    		Fl_get_color_rgb(value, ref r, ref g, ref b);
    		_log.Trace($"{nameof(Fl_get_color_rgb)}(0x{value:x2}) -> r: 0x{(byte)r:x2}, g: 0x{(byte)g:x2}, b: 0x{(byte)b:x2}");
    
    		return Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_rgb_color)}(0x{value.R:x2}, 0x{value.G:x2}, 0x{value.B:x2})");
    		var data = Fl_rgb_color((char)value.R, (char)value.G, (char)value.B);
    		_log.Trace($"{nameof(Fl_Window_set_selection_color)}(0x{Pointer:x2}, 0x{data:x2})");
    		Fl_Window_set_selection_color(Pointer, data);
    	}
    }
    #endregion
    #region CodeTemplate.IFltkGroup.txt
    public void Begin()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_begin)}(0x{Pointer:x2})");
        Fl_Window_begin(Pointer);
    }
    public void End()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_end)}(0x{Pointer:x2})");
        Fl_Window_end(Pointer);
    }
    public int IndexOfChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Window_find)}(0x{Pointer:x2}, 0x{ptr:x2})");
        var r = Fl_Window_find(Pointer, ptr);
        return r;
    }
    public void AddChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Window_add)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Window_add(Pointer, ptr);
    }
    public void InsertChildAt(IFltkWidget child, int index)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Window_insert)}(0x{Pointer:x2}, 0x{ptr:x2}, {index})");
        Fl_Window_insert(Pointer, ptr, index);
    }
    public void RemoveChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Window_remove)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Window_remove(Pointer, ptr);
    }
    public void RemoveChild(int index)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_remove_by_index)}(0x{Pointer:x2}, {index})");
        Fl_Window_remove_by_index(Pointer, index);
    }
    public void RemoveAllChildren()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_clear)}(0x{Pointer:x2})");
        Fl_Window_clear(Pointer);
    }
    public int ChildCount()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_children)}(0x{Pointer:x2})");
        var r = Fl_Window_children(Pointer);
        return r;
    }
    public object? ChildAt<T>(int index) where T : IFltkWidget
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_child)}(0x{Pointer:x2}, {index})");
        var ptr = Fl_Window_child(Pointer, index);
        if (ptr == IntPtr.Zero)
            return null;
        return T.FromPointer(ptr);
    }
    
    public bool ClipChildren
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_clip_children)}(0x{Pointer:x2})");
            var r = Fl_Window_clip_children(Pointer);
            return r != 0;
        }
        set
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            var v = value ? 1 : 0;
            _log.Trace($"{nameof(Fl_Window_set_clip_children)}(0x{Pointer:x2}, {v})");
            Fl_Window_set_clip_children(Pointer, v);
        }
    }
    
    public void InitializeSizes()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_init_sizes)}(0x{Pointer:x2})");
        Fl_Window_init_sizes(Pointer);
    }
    public void DrawChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        if (ptr == IntPtr.Zero)
            return;
        _log.Trace($"{nameof(Fl_Window_draw_child)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Window_draw_child(Pointer, ptr);
    }
    public void UpdateChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        if (ptr == IntPtr.Zero)
            return;
        _log.Trace($"{nameof(Fl_Window_update_child)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Window_update_child(Pointer, ptr);
    }
    public void DrawOutsideLabel(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        if (ptr == IntPtr.Zero)
            return;
        _log.Trace($"{nameof(Fl_Window_draw_outside_label)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Window_draw_outside_label(Pointer, ptr);
    }
    public void DrawChildren()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_draw_children)}(0x{Pointer:x2})");
        Fl_Window_draw_children(Pointer);
    }
    #endregion
    #region CodeTemplate.IFltkWindow.txt
    public void MakeModal(bool state)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_make_modal)}(0x{Pointer:x2}, {state})");
        Fl_Window_make_modal(Pointer, state);
    }
    public void Fullscreen(bool state)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_fullscreen)}(0x{Pointer:x2}, {state})");
        Fl_Window_fullscreen(Pointer, state);
    }
    public void MakeCurrent()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_make_current)}(0x{Pointer:x2})");
        Fl_Window_make_current(Pointer);
    }
    public IntPtr Icon
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_icon)}(0x{Pointer:x2})");
            return Fl_Window_icon(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_icon)}(0x{Pointer:x2}, 0x{value:x2})");
            Fl_Window_set_icon(Pointer, value);
        }
    }
    
    public bool Visible
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_shown)}(0x{Pointer:x2})");
            return Fl_Window_shown(Pointer) != 0;
        }
    }
    
    public int Border
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_border)}(0x{Pointer:x2})");
            return Fl_Window_border(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_set_border)}(0x{Pointer:x2}, 0x{value:x2})");
            Fl_Window_set_border(Pointer, value);
        }
    }
    
    public IntPtr Region
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_region)}(0x{Pointer:x2})");
            return Fl_Window_region(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_set_region)}(0x{Pointer:x2}, 0x{value:x2})");
            Fl_Window_set_region(Pointer, value);
        }
    }
    
    public void Iconize()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_iconize)}(0x{Pointer:x2})");
        Fl_Window_iconize(Pointer);
    }
    
    public bool FullscreenActive
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_fullscreen_active)}(0x{Pointer:x2})");
            return Fl_Window_fullscreen_active(Pointer) != 0;
        }
    }
    
    public void FreePosition()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_free_position)}(0x{Pointer:x2})");
        Fl_Window_free_position(Pointer);
    }
    
    public Size DecoratedSize
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_decorated_w)}(0x{Pointer:x2})");
            int w = Fl_Window_decorated_w(Pointer);
            _log.Trace($"{nameof(Fl_Window_decorated_h)}(0x{Pointer:x2})");
            int h = Fl_Window_decorated_h(Pointer);
            return new(w, h);
        }
    }
    
    public void SetSizeRange(Size min, Size max)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_size_range)}(0x{Pointer:x2}, {min.Width}, {min.Height}, {max.Width}, {max.Height})");
        Fl_Window_size_range(Pointer, min.Width, min.Height, max.Width, max.Height);
    }
    
    public void SetHotspot(IFltkWidget? target)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = target?.GetPointer() ?? IntPtr.Zero;
        if (ptr == IntPtr.Zero)
        {
            _log.Trace($"{nameof(Fl_Window_hotspot)}(0x{Pointer:x2}, NULL)");
        }
        else
        {
            _log.Trace($"{nameof(Fl_Window_hotspot)}(0x{Pointer:x2}, 0x{ptr:x2})");
        }
        Fl_Window_hotspot(Pointer, ptr);
    }
    
    public IFltkImage? Shape
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_shape)}(0x{Pointer:x2})");
            var ptr = Fl_Window_shape(Pointer);
            if (ptr == IntPtr.Zero)
                return null;
            return (IFltkImage)new FLImage(ptr);
        }
        set
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            var ptr = value?.GetPointer() ?? IntPtr.Zero;
            if (ptr == IntPtr.Zero)
            {
                _log.Trace($"{nameof(Fl_Window_set_shape)}(0x{Pointer:x2}, NULL)");
                Fl_Window_set_shape(Pointer, IntPtr.Zero);
            }
            else
            {
                _log.Trace($"{nameof(Fl_Window_set_shape)}(0x{Pointer:x2}, 0x{ptr:x2})");
                Fl_Window_set_shape(Pointer, ptr);
            }
        }
    }
    
    public Point RootPosition
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_x_root)}(0x{Pointer:x2})");
            int x = Fl_Window_x_root(Pointer);
            _log.Trace($"{nameof(Fl_Window_y_root)}(0x{Pointer:x2})");
            int y = Fl_Window_y_root(Pointer);
            return new(x, y);
        }
    }
    
    public void SetCursorImage(IFltkRgbImage? image, Point hot)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = image?.GetPointer() ?? IntPtr.Zero;
        if (ptr == IntPtr.Zero)
        {
            _log.Trace($"{nameof(Fl_Window_set_cursor_image)}(0x{Pointer:x2}, NULL, {hot.X}, {hot.Y})");
        }
        else
        {
            _log.Trace($"{nameof(Fl_Window_set_cursor_image)}(0x{Pointer:x2}, 0x{ptr:x2}, {hot.X}, {hot.Y})");
        }
        Fl_Window_set_cursor_image(Pointer, ptr, hot.X, hot.Y);
    }
    
    public void DefaultCursor(FltkCursorKind kind)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var x = (int)kind;
        _log.Trace($"{nameof(Fl_Window_default_cursor)}(0x{Pointer:x2}, 0x{x:x2})");
        Fl_Window_default_cursor(Pointer, x);
    }
    
    public int ScreenNumber
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_screen_num)}(0x{Pointer:x2})");
            return Fl_Window_screen_num(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_set_screen_num)}(0x{Pointer:x2}, {value})");
            Fl_Window_set_screen_num(Pointer, value);
        }
    }
    
    public void WaitForExpose()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_wait_for_expose)}(0x{Pointer:x2})");
        Fl_Window_wait_for_expose(Pointer);
    }
    
    public byte Alpha
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_alpha)}(0x{Pointer:x2})");
            var a = Fl_Window_alpha(Pointer);
            return (byte)a;
        }
        set
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            var a = (char)value;
            _log.Trace($"{nameof(Fl_Window_set_alpha)}(0x{Pointer:x2}, 0x{value:x2})");
            Fl_Window_set_alpha(Pointer, a);
        }
    }
    
    public void ForcePosition(int flag)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_force_position)}(0x{Pointer:x2}, 0x{flag:x2})");
        Fl_Window_force_position(Pointer, flag);
    }
    
    public static string? DefaultXClass
    {
        get
        {
            var log = LogManager.GetLogger("FLWindow");
            log.Trace($"{nameof(Fl_Window_default_xclass)}()");
            var ptr = Fl_Window_default_xclass();
            return InternalHelper.ReadString(ptr);
        }
        set
        {
            var log = LogManager.GetLogger("FLWindow");
            var l = new List<Action>();
            var ptr = InternalHelper.AllocateString(value, l);
            log.Trace($"{nameof(Fl_Window_set_default_xclass)}(0x{ptr:x2})");
            Fl_Window_set_default_xclass(ptr);
        }
    }
    
    public string? XClass
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_xclass)}(0x{Pointer:x2})");
            var ptr = Fl_Window_xclass(Pointer);
            return InternalHelper.ReadString(ptr);
        }
        set
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            var ptr = InternalHelper.AllocateString(value, _disposeActions);
            _log.Trace($"{nameof(Fl_Window_set_xclass)}(0x{Pointer:x2}, 0x{ptr:x2})");
            Fl_Window_set_xclass(Pointer, ptr);
        }
    }
    
    public void ClearModalStates()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_clear_modal_states)}(0x{Pointer:x2})");
        Fl_Window_clear_modal_states(Pointer);
    }
    
    public string? IconLabel
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_icon_label)}(0x{Pointer:x2})");
            var ptr = Fl_Window_icon_label(Pointer);
            return InternalHelper.ReadString(ptr);
        }
        set
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            var ptr = InternalHelper.AllocateString(value, _disposeActions);
            _log.Trace($"{nameof(Fl_Window_set_icon_label)}(0x{Pointer:x2}, 0x{ptr:x2})");
            Fl_Window_set_icon_label(Pointer, ptr);
        }
    }
    
    public void SetIcons<T>(List<T> images) where T : IFltkRgbImage
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        if (images.Count < 1)
        {
            _log.Trace($"{nameof(Fl_Window_set_icons)}(0x{Pointer:x2}, NULL, 0)");
            Fl_Window_set_icons(Pointer, IntPtr.Zero, 0);
            return;
        }
        var ptrArray = new IntPtr[images.Count];
        for (int i = 0; i < images.Count; i++)
            ptrArray[i] = images[i].GetPointer();
        var ptr = InternalHelper.AllocateGlobal(ptrArray, _disposeActions);
        _log.Trace($"{nameof(Fl_Window_set_icons)}(0x{Pointer:x2}, 0x{ptr:x2}, {ptrArray.Length})");
        Fl_Window_set_icons(Pointer, ptr, ptrArray.Length);
    }
    public void Maximize()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_maximize)}(0x{Pointer:x2})");
        Fl_Window_maximize(Pointer);
    }
    public void Minimize()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Window_un_maximize)}(0x{Pointer:x2})");
        Fl_Window_un_maximize(Pointer);
    }
    public bool IsMaximized
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Window_maximize_active)}(0x{Pointer:x2})");
            return Fl_Window_maximize_active(Pointer) != 0;
        }
    }
    #endregion   
}
#nullable disable