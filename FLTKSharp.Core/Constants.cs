namespace FLTKSharp.Core;

public class Constants
{
    public const string LibraryFilename =
#if _WINDOWS
    "cfltk.dll";
#elif _LINUX
    "cfltk.so";
#else
    "cfltk";
#endif

    public const InternalStringCharacterSet StringCharset = InternalStringCharacterSet.ANSI;
    public enum InternalStringCharacterSet
    {
        ANSI,
        Unicode,
        Auto
    }
}
