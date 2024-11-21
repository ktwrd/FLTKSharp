using NLog;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core;

public class FLTK
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    public static void Initialize()
    {
#if DEBUG
        Console.WriteLine($"ABI: {Fl_abi_version()}, API: {Fl_api_version()}, Version: {Fl_version()}");
#endif
        Log.Trace(nameof(Fl_init_all));
        Fl_init_all();
    }

    private static void ExtractLibraries()
    {
        var possibleFiles = new string[]
        {
            "cfltk.dll",
            "cfltk.so",
            "cfltk.dylib",
            "libcfltk.so",
            "libcfltk.dylib",
            "cfltk"
        };
        bool exists = false;
        foreach (var x in possibleFiles)
        {
            if (File.Exists(x))
            {
                exists = true;
                break;
            }
        }

        if (exists)
        {
            Log.Trace($"Library already exists. Extraction is not necessary.");
            return;
        }
        
        var libraryDirectory = Environment.GetEnvironmentVariable("FLTKSharp.Core.LibraryDirectory");
        if (string.IsNullOrEmpty(libraryDirectory))
        {
            #if _WINDOWS
            libraryDirectory = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "FLTKSharp.Core",
                "Library");
            #else
            libraryDirectory = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".local",
                "FLTKSharp.Core",
                "Library");
            #endif
        }

        if (!Directory.Exists(libraryDirectory))
        {
            Log.Trace($"Created directory {libraryDirectory}");
            Directory.CreateDirectory(libraryDirectory);
        }
        
        Log.Trace($"Extracting libraries to {libraryDirectory}");
    }

    public static void ThreadLock()
    {
        Log.Trace(nameof(Fl_lock));
        Fl_lock();
    }
    public static void ThreadResume()
    {
        Log.Trace(nameof(Fl_awake));
        Fl_awake();
        Log.Trace(nameof(Fl_unlock));
        Fl_unlock();
    }
    public static void Run()
    {
        Log.Trace(nameof(Fl_run));
        Fl_run();
    }
}
