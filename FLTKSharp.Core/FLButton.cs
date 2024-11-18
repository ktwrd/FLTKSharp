using System.Runtime.InteropServices;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLButton : BaseFltkObject
{
    public FLButton(int width, int height, string text = "")
        : this(0, 0, width, height, text)
    { }
    public FLButton(int x, int y, int width, int height, string text = "")
        : base(Generate(x, y, width, height, text, out var dd))
    {
        _disposeActions.Add(dd);
    }

    private static IntPtr Generate(int x, int y, int width, int height, string text, out Action disposeDelegate)
    {
        var textPointer = Marshal.StringToHGlobalAnsi(text);
        disposeDelegate = () => Marshal.FreeHGlobal(textPointer);
        return Fl_Button_new(x, y, width, height, textPointer);
    }

    public void Handle<T>(Func<int, object?, bool> @delegate, T? obj)
        where T : BaseFltkObject
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }

        IntPtr ptr = IntPtr.Zero;
        if (obj != null)
            ptr = obj.Pointer;
        FltkButtonHandleCallback cb = (_, e, o) =>
        {
            BaseFltkObject? x = null;
            if (o == ptr)
            {
                x = new(o);
            }
            return @delegate(e, x) ? 1 : 0;
        };
        Fl_Button_handle(this.Pointer, cb, ptr);
    }
    public void Handle(Func<int, bool> @delegate)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }

        FltkButtonHandleCallback cb = (_, e, _) =>
        {
            return @delegate(e) ? 1 : 0;
        };
        Fl_Button_handle(this.Pointer, cb, IntPtr.Zero);
    }

    public void SetColor(int red, int green, int blue)
    {
        if (Pointer == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Property {nameof(IntPtr.Zero)} is a null pointer");
        }

        var value = Convert.ToInt32(string.Format("0x{0:X2}{1:X2}{2:X2}00", red, green, blue), 16);
        Fl_Button_set_color(Pointer, value);
    }
}
