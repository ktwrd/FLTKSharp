using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLTK
{
    public static void Initialize()
    {
        Fl_init_all();
    }

    public static void ThreadLock()
    {
        Fl_lock();
    }
    public static void ThreadResume()
    {
        Fl_awake();
        Fl_unlock();
    }
    public static void Run()
    {
        Fl_run();
    }
}
