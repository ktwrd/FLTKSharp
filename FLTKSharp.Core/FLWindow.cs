using System.Runtime.InteropServices;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLWindow : FLGroup
{
    public FLWindow(int width, int height, string title = "")
        : this(0, 0, width, height, title)
    {
    }

    public FLWindow(int x, int y, int width, int height, string title = "")
        : base(Create(x, y, width, height, title, out var dd))
    {
        _disposeActions.Add(dd);
    }

    internal FLWindow(IntPtr pointer)
        : base(pointer)
    {
        base.FlObjectHandle = Fl_Window_handle;
    }

    protected override FltkObjectHandleMethod FlObjectHandle
    {
        get => base.FlObjectHandle;
        set => base.FlObjectHandle = value;
    }
    
    private static IntPtr Create(int x, int y, int width, int height, string title, out Action disposeAction)
    {
        var titlePointer = Marshal.StringToHGlobalAnsi(title);
        disposeAction = () => Marshal.FreeHGlobal(titlePointer);
        return Fl_Window_new(x, y, width, height, titlePointer);
    }

    public override void Show()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Window_show(Pointer);
    }
    public override void Hide()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Window_hide(Pointer);
    }

    public override void Begin()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Window_begin(Pointer);
    }
    public override void End()
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }
        Fl_Window_end(Pointer);
    }

    public int Height
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            return Fl_Window_height(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            var posx = Fl_Window_x(Pointer);
            var posy = Fl_Window_y(Pointer);
            var sizew = Fl_Window_width(Pointer);
            Fl_Window_resize(Pointer, posx, posy, sizew, value);
        }
    }

    public int Width
    {
        get
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            return Fl_Window_width(Pointer);
        }
        set
        {
            if (Pointer == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
            }
            var posx = Fl_Window_x(Pointer);
            var posy = Fl_Window_y(Pointer);
            var sizeh = Fl_Window_height(Pointer);
            Fl_Window_resize(Pointer, posx, posy, value, sizeh);
        }
    }
}
