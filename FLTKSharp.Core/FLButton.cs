using FLTKSharp.Core.Events;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLButton : BaseFltkObject
{
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
        FlObjectHandle = Fl_Button_handle;
        base.UnsafeEvent += FLButton_UnsafeEvent;
        base.Disposed += FLButton_Disposed;
    }

    private void FLButton_Disposed(object? sender, EventArgs e)
    {
        UnsafeEvent -= FLButton_UnsafeEvent;
    }

    private void FLButton_UnsafeEvent(object? sender, FltkUnsafeEventArgs e)
    {
        Trace.WriteLine(e.Event);
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

    protected override FltkObjectHandleMethod FlObjectHandle
    {
        get => base.FlObjectHandle;
        set => base.FlObjectHandle = value;
    }

    public event EventHandler<FltkMouseEventArgs>? MouseDown;
    public event EventHandler<FltkMouseEventArgs>? MouseUp;

    private static IntPtr Generate(int x, int y, int width, int height, string text, out Action disposeDelegate)
    {
        var textPointer = Marshal.StringToHGlobalAnsi(text);
        disposeDelegate = () => Marshal.FreeHGlobal(textPointer);
        return Fl_Button_new(x, y, width, height, textPointer);
    }
    public void SetColor(Color color)
    {
        SetColor(color.R, color.G, color.B, Convert.ToByte(byte.MaxValue - color.A));
    }
    public void SetColor(byte red, byte green, byte blue)
    {
        SetColor(red, green, blue, 0xff);
    }
    public void SetColor(byte red, byte green, byte blue, byte alpha)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }

        var value = Convert.ToInt32(string.Format("0x{0:X2}{1:X2}{2:X2}{3:X2}", red, green, blue, alpha), 16);
        Fl_Button_set_color(Pointer, value);
    }

    public bool IsCompact
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            var value = Fl_Button_compact(Pointer);
            return value == '1';
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            Fl_Button_set_compact(Pointer, value ? '1' : '0');
        }
    }

    public FltkBoxType BoxType
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            var value = Fl_Button_down_box(Pointer);
            return (FltkBoxType)value;
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            Fl_Button_set_down_box(Pointer, (int)value);
        }
    }

    public void Redraw()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Button_redraw(Pointer);
    }

    public int ShortcutKey
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            return Fl_Button_shortcut(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            Fl_Button_set_shortcut(Pointer, value);
        }
    }
}
