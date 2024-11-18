namespace FLTKSharp.Core;

public class Constants
{
    public const string LibraryFilename =
#if _WINDOWS
    "cfltk.dll";
#elif _LINUX
    "cfltk.so";
#else
    "cfltk.exported_library";
#endif
}
