using System.Runtime.InteropServices;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLWindow : BaseFltkObject
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

    private static IntPtr Create(int x, int y, int width, int height, string title, out Action disposeAction)
    {
        var titlePointer = Marshal.StringToHGlobalAnsi(title);
        disposeAction = () => Marshal.FreeHGlobal(titlePointer);
        return Fl_Window_new(x, y, width, height, titlePointer);
    }

    public void Show()
    {
        Fl_Window_show(Pointer);
    }
    public void Hide()
    {
        Fl_Window_hide(Pointer);
    }
    public void End()
    {
        Fl_Window_end(Pointer);
    }

    public int Height
    {
        get
        {
            return Fl_Window_height(Pointer);
        }
    }
}
