using System.Runtime.InteropServices;

namespace FLTKSharp.Core;

public struct FunctionPointer<TFunc>
    where TFunc : notnull
{
    public IntPtr Pointer;

    public FunctionPointer(TFunc func)
    {
        Pointer = Marshal.GetFunctionPointerForDelegate(func);
    }
}