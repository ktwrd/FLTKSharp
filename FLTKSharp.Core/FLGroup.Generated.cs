using System.Drawing;
using FLTKSharp.Core.Interfaces;
using NLog;
using static FLTKSharp.Core.CFltkNative;


namespace FLTKSharp.Core;
#nullable enable
public partial class FLGroup : FLTKSharp.Core.BaseFltkEventedObject, FLTKSharp.Core.Interfaces.IFltkWidget, FLTKSharp.Core.Interfaces.IFltkGroup
{
    public static IFltkWidget FromPointer(IntPtr pointer) => new FLGroup(pointer);
    public IntPtr GetPointer() => Pointer;
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
        : this(Create(x, y, width, height, label, out var disposeAction))
    {
        _disposeActions.Add(disposeAction);
    }
    internal FLGroup(IntPtr pointer)
        : base(pointer)
    {
        _log.Properties["Pointer"] = pointer;
        _log.Trace($"Created " + pointer.ToString("x2"));
        base.FlObjectHandle = Fl_Group_handle;
    }
    protected override FltkObjectHandleMethod FlObjectHandle
    {
        get => base.FlObjectHandle;
        set => base.FlObjectHandle = value;
    }
    
    private static IntPtr Create(int x, int y, int width, int height, string? label, out Action disposeAction)
    {
        var labelPointer = InternalHelper.AllocateStringD(label, out disposeAction);
        return Fl_Group_new(x, y, width, height, labelPointer);
    }

    #region CodeTemplate.IFltkWidget.txt
    public Point Position
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_x)}(0x{Pointer:x2})");
    		var x = Fl_Group_x(Pointer);
    		_log.Trace($"{nameof(Fl_Group_x)}(0x{Pointer:x2})");
    		var y = Fl_Group_y(Pointer);
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
    		_log.Trace($"{nameof(Fl_Group_width)}(0x{Pointer:x2})");
    		var w = Fl_Group_width(Pointer);
    		_log.Trace($"{nameof(Fl_Group_height)}(0x{Pointer:x2})");
    		var h = Fl_Group_height(Pointer);
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
    		_log.Trace($"{nameof(Fl_Group_label)}(0x{Pointer:x2})");
    		var ptr = Fl_Group_label(Pointer);
    		return InternalHelper.ReadString(ptr);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		var ptr = InternalHelper.AllocateString(value, _disposeActions);
    		_log.Trace($"{nameof(Fl_Group_set_label)}(0x{Pointer:x2}, 0x{ptr:x2})");
    		Fl_Group_set_label(Pointer, ptr);
    	}
    }
    
    public void Redraw()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Group_redraw)}(0x{Pointer:x2})");
    	Fl_Group_redraw(Pointer);
    }
    
    public void Show()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Group_show)}(0x{Pointer:x2})");
    	Fl_Group_show(Pointer);
    }
    
    public void Hide()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Group_hide)}(0x{Pointer:x2})");
    	Fl_Group_hide(Pointer);
    }
    
    public bool Enabled
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_active)}(0x{Pointer:x2})");
    		return Fl_Group_active(Pointer) != 0;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_active)}(0x{Pointer:x2})");
    		if (Fl_Group_active(Pointer) == 0)
    		{
    			_log.Trace($"{nameof(Fl_Group_activate)}(0x{Pointer:x2})");
    			Fl_Group_activate(Pointer);
    		}
    		else
    		{
    			_log.Trace($"{nameof(Fl_Group_deactivate)}(0x{Pointer:x2})");
    			Fl_Group_deactivate(Pointer);
    		}
    	}
    }
    
    public void Resize()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Group_redraw)}(0x{Pointer:x2})");
    	Fl_Group_redraw(Pointer);
    }
    
    public void RedrawLabel()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Group_redraw_label)}(0x{Pointer:x2})");
    	Fl_Group_redraw_label(Pointer);
    }
    
    public void Resize(int x, int y, int width, int height)
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Group_resize)}(0x{Pointer:x2}, {x}, {y}, {width}, {height})");
    	Fl_Group_resize(Pointer, x, y, width, height);
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
    		_log.Trace($"{nameof(Fl_Group_tooltip)}(0x{Pointer:x2})");
    		var ptr = Fl_Group_tooltip(Pointer);
    		return InternalHelper.ReadString(ptr);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		var ptr = InternalHelper.AllocateString(value, _disposeActions);
    		_log.Trace($"{nameof(Fl_Group_set_tooltip)}(0x{Pointer:x2}, 0x{ptr:x2})");
    		Fl_Group_set_tooltip(Pointer, ptr);
    	}
    }
    
    public int LabelFont
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_label_font)}(0x{Pointer:x2})");
    		return Fl_Group_label_font(Pointer);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_set_label_font)}(0x{Pointer:x2}, 0x{value:0x})");
    		Fl_Group_set_label_font(Pointer, value);
    	}
    }
    
    public int LabelSize
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_label_size)}(0x{Pointer:x2})");
    		return Fl_Group_label_size(Pointer);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_set_label_size)}(0x{Pointer:x2}, {value})");
    		Fl_Group_set_label_size(Pointer, value);
    	}
    }
    
    public FltkLabelType LabelType
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_label_type)}(0x{Pointer:x2})");
    		var result = Fl_Group_label_type(Pointer);
    		return (FltkLabelType)result;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_set_label_type)}(0x{Pointer:x2}, {(int)value})");
    		Fl_Group_set_label_type(Pointer, (int)value);
    	}
    }
    
    public Color LabelColor
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		var c = Fl_Group_label_color(Pointer);
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
    
    		_log.Trace($"{nameof(Fl_Group_set_label_color)}(0x{Pointer:x2}, 0x{c:x2})");
    		Fl_Group_set_label_color(Pointer, c);
    	}
    }
    
    public Size LabelDimensions
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		int w = 0;
    		int h = 0;
    		_log.Trace($"{nameof(Fl_Group_measure_label)}(0x{Pointer:x2})");
    		Fl_Group_measure_label(Pointer, ref w, ref h);
    		_log.Trace($"{nameof(Fl_Group_measure_label)}(0x{Pointer:x2}) -> w: {w}, h: {h}");
    
    		return new Size(w, h);
    	}
    }
    
    public FltkBoxType BoxType
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_box)}(0x{Pointer:x2})");
    		var value = Fl_Group_box(Pointer);
    		return (FltkBoxType)value;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_set_box)}(0x{Pointer:x2}, 0x{(int)value:x2})");
    		Fl_Group_set_box(Pointer, (int)value);
    	}
    }
    
    
    public FltkAlign Alignment
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_align)}(0x{Pointer:x2})");
    		var value = Fl_Group_align(Pointer);
    		var u = Convert.ToUInt16(value);
    		return (FltkAlign)u;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_set_align)}(0x{Pointer:x2}, 0x{(int)value:x2})");
    		Fl_Group_set_align(Pointer, (int)value);
    	}
    }
    
    public void Delete()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Group_delete)}(0x{Pointer:x2})");
    	Fl_Group_delete(Pointer);
    }
    
    public Color SelectionColor
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Group_selection_color)}(0x{Pointer:x2})");
    		var value = Fl_Group_selection_color(Pointer);
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
    		_log.Trace($"{nameof(Fl_Group_set_selection_color)}(0x{Pointer:x2}, 0x{data:x2})");
    		Fl_Group_set_selection_color(Pointer, data);
    	}
    }
    #endregion
    #region CodeTemplate.IFltkGroup.txt
    public void Begin()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Group_begin)}(0x{Pointer:x2})");
        Fl_Group_begin(Pointer);
    }
    public void End()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Group_end)}(0x{Pointer:x2})");
        Fl_Group_end(Pointer);
    }
    public int IndexOfChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Group_find)}(0x{Pointer:x2}, 0x{ptr:x2})");
        var r = Fl_Group_find(Pointer, ptr);
        return r;
    }
    public void AddChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Group_add)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Group_add(Pointer, ptr);
    }
    public void InsertChildAt(IFltkWidget child, int index)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Group_insert)}(0x{Pointer:x2}, 0x{ptr:x2}, {index})");
        Fl_Group_insert(Pointer, ptr, index);
    }
    public void RemoveChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        _log.Trace($"{nameof(Fl_Group_remove)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Group_remove(Pointer, ptr);
    }
    public void RemoveChild(int index)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Group_remove_by_index)}(0x{Pointer:x2}, {index})");
        Fl_Group_remove_by_index(Pointer, index);
    }
    public void RemoveAllChildren()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Group_clear)}(0x{Pointer:x2})");
        Fl_Group_clear(Pointer);
    }
    public int ChildCount()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Group_children)}(0x{Pointer:x2})");
        var r = Fl_Group_children(Pointer);
        return r;
    }
    public object? ChildAt<T>(int index) where T : IFltkWidget
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Group_child)}(0x{Pointer:x2}, {index})");
        var ptr = Fl_Group_child(Pointer, index);
        if (ptr == IntPtr.Zero)
            return null;
        return T.FromPointer(ptr);
    }
    
    public bool ClipChildren
    {
        get
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            _log.Trace($"{nameof(Fl_Group_clip_children)}(0x{Pointer:x2})");
            var r = Fl_Group_clip_children(Pointer);
            return r != 0;
        }
        set
        {
            if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
            var v = value ? 1 : 0;
            _log.Trace($"{nameof(Fl_Group_set_clip_children)}(0x{Pointer:x2}, {v})");
            Fl_Group_set_clip_children(Pointer, v);
        }
    }
    
    public void InitializeSizes()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Group_init_sizes)}(0x{Pointer:x2})");
        Fl_Group_init_sizes(Pointer);
    }
    public void DrawChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        if (ptr == IntPtr.Zero)
            return;
        _log.Trace($"{nameof(Fl_Group_draw_child)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Group_draw_child(Pointer, ptr);
    }
    public void UpdateChild(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        if (ptr == IntPtr.Zero)
            return;
        _log.Trace($"{nameof(Fl_Group_update_child)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Group_update_child(Pointer, ptr);
    }
    public void DrawOutsideLabel(IFltkWidget child)
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        var ptr = child.GetPointer();
        if (ptr == IntPtr.Zero)
            return;
        _log.Trace($"{nameof(Fl_Group_draw_outside_label)}(0x{Pointer:x2}, 0x{ptr:x2})");
        Fl_Group_draw_outside_label(Pointer, ptr);
    }
    public void DrawChildren()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
        _log.Trace($"{nameof(Fl_Group_draw_children)}(0x{Pointer:x2})");
        Fl_Group_draw_children(Pointer);
    }
    #endregion
}
#nullable disable