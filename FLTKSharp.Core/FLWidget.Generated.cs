using System.Drawing;
using FLTKSharp.Core.Interfaces;
using NLog;
using static FLTKSharp.Core.CFltkNative;


namespace FLTKSharp.Core;

#nullable enable
public partial class FLWidget : FLTKSharp.Core.BaseFltkEventedObject, FLTKSharp.Core.Interfaces.IFltkWidget
{
    public static IFltkWidget FromPointer(IntPtr pointer) => new FLWidget(pointer);
    public IntPtr GetPointer() => Pointer;
    private readonly Logger _log = LogManager.GetCurrentClassLogger();
    
    public FLWidget(int x, int y, int width, int height, string? label = null)
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
    
    private static IntPtr Create(int x, int y, int width, int height, string? label, out Action disposeAction)
    {
        var labelPointer = InternalHelper.AllocateStringD(label, out disposeAction);
        return Fl_Widget_new(x, y, width, height, labelPointer);
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
    
    public FLWindow AsWindow()
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

    #region CodeTemplate.IFltkWidget.txt
    public Point Position
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_x)}(0x{Pointer:x2})");
    		var x = Fl_Widget_x(Pointer);
    		_log.Trace($"{nameof(Fl_Widget_x)}(0x{Pointer:x2})");
    		var y = Fl_Widget_y(Pointer);
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
    		_log.Trace($"{nameof(Fl_Widget_width)}(0x{Pointer:x2})");
    		var w = Fl_Widget_width(Pointer);
    		_log.Trace($"{nameof(Fl_Widget_height)}(0x{Pointer:x2})");
    		var h = Fl_Widget_height(Pointer);
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
    		_log.Trace($"{nameof(Fl_Widget_label)}(0x{Pointer:x2})");
    		var ptr = Fl_Widget_label(Pointer);
    		return InternalHelper.ReadString(ptr);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		var ptr = InternalHelper.AllocateString(value, _disposeActions);
    		_log.Trace($"{nameof(Fl_Widget_set_label)}(0x{Pointer:x2}, 0x{ptr:x2})");
    		Fl_Widget_set_label(Pointer, ptr);
    	}
    }
    
    public void Redraw()
    {
        if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Widget_redraw)}(0x{Pointer:x2})");
    	Fl_Widget_redraw(Pointer);
    }
    
    public void Show()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Widget_show)}(0x{Pointer:x2})");
    	Fl_Widget_show(Pointer);
    }
    
    public void Hide()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Widget_hide)}(0x{Pointer:x2})");
    	Fl_Widget_hide(Pointer);
    }
    
    public bool Enabled
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_active)}(0x{Pointer:x2})");
    		return Fl_Widget_active(Pointer) != 0;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
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
    
    public void Resize()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Widget_redraw)}(0x{Pointer:x2})");
    	Fl_Widget_redraw(Pointer);
    }
    
    public void RedrawLabel()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Widget_redraw_label)}(0x{Pointer:x2})");
    	Fl_Widget_redraw_label(Pointer);
    }
    
    public void Resize(int x, int y, int width, int height)
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
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
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_tooltip)}(0x{Pointer:x2})");
    		var ptr = Fl_Widget_tooltip(Pointer);
    		return InternalHelper.ReadString(ptr);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		var ptr = InternalHelper.AllocateString(value, _disposeActions);
    		_log.Trace($"{nameof(Fl_Widget_set_tooltip)}(0x{Pointer:x2}, 0x{ptr:x2})");
    		Fl_Widget_set_tooltip(Pointer, ptr);
    	}
    }
    
    public int LabelFont
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_label_font)}(0x{Pointer:x2})");
    		return Fl_Widget_label_font(Pointer);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_set_label_font)}(0x{Pointer:x2}, 0x{value:0x})");
    		Fl_Widget_set_label_font(Pointer, value);
    	}
    }
    
    public int LabelSize
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_label_size)}(0x{Pointer:x2})");
    		return Fl_Widget_label_size(Pointer);
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_set_label_size)}(0x{Pointer:x2}, {value})");
    		Fl_Widget_set_label_size(Pointer, value);
    	}
    }
    
    public FltkLabelType LabelType
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_label_type)}(0x{Pointer:x2})");
    		var result = Fl_Widget_label_type(Pointer);
    		return (FltkLabelType)result;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_set_label_type)}(0x{Pointer:x2}, {(int)value})");
    		Fl_Widget_set_label_type(Pointer, (int)value);
    	}
    }
    
    public Color LabelColor
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		var c = Fl_Widget_label_color(Pointer);
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
    
    		_log.Trace($"{nameof(Fl_Widget_set_label_color)}(0x{Pointer:x2}, 0x{c:x2})");
    		Fl_Widget_set_label_color(Pointer, c);
    	}
    }
    
    public Size LabelDimensions
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		int w = 0;
    		int h = 0;
    		_log.Trace($"{nameof(Fl_Widget_measure_label)}(0x{Pointer:x2})");
    		Fl_Widget_measure_label(Pointer, ref w, ref h);
    		_log.Trace($"{nameof(Fl_Widget_measure_label)}(0x{Pointer:x2}) -> w: {w}, h: {h}");
    
    		return new Size(w, h);
    	}
    }
    
    public FltkBoxType BoxType
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_box)}(0x{Pointer:x2})");
    		var value = Fl_Widget_box(Pointer);
    		return (FltkBoxType)value;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_set_box)}(0x{Pointer:x2}, 0x{(int)value:x2})");
    		Fl_Widget_set_box(Pointer, (int)value);
    	}
    }
    
    
    public FltkAlign Alignment
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_align)}(0x{Pointer:x2})");
    		var value = Fl_Widget_align(Pointer);
    		var u = Convert.ToUInt16(value);
    		return (FltkAlign)u;
    	}
    	set
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_set_align)}(0x{Pointer:x2}, 0x{(int)value:x2})");
    		Fl_Widget_set_align(Pointer, (int)value);
    	}
    }
    
    public void Delete()
    {
    	if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    	_log.Trace($"{nameof(Fl_Widget_delete)}(0x{Pointer:x2})");
    	Fl_Widget_delete(Pointer);
    }
    
    public Color SelectionColor
    {
    	get
    	{
    		if (Pointer == IntPtr.Zero) { throw new InvalidOperationException($"Property {nameof(Pointer)} is a null pointer"); }
    		_log.Trace($"{nameof(Fl_Widget_selection_color)}(0x{Pointer:x2})");
    		var value = Fl_Widget_selection_color(Pointer);
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
    		_log.Trace($"{nameof(Fl_Widget_set_selection_color)}(0x{Pointer:x2}, 0x{data:x2})");
    		Fl_Widget_set_selection_color(Pointer, data);
    	}
    }
    #endregion
}
#nullable disable