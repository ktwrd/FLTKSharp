using System.Reflection;
using System.Runtime.InteropServices;
using NLog;
using static FLTKSharp.Core.Constants;

namespace FLTKSharp.Core
{
    internal partial class InternalHelper
    {
        internal static IntPtr AllocateStringD(string? value, out Action disposeAction, bool globalAllocate = true)
        {
            var log = LogManager.GetLogger("AllocateStringD");
            IntPtr ptr = IntPtr.Zero;
            
            if (!string.IsNullOrEmpty(value))
            {
                ptr = GetStringAllocater(globalAllocate)(value);
                disposeAction = () => Marshal.FreeHGlobal(ptr);
            }
            else
            {
                disposeAction = () => { };
            }
            log.Trace($"Allocated \"{value}\" to 0x" + ptr.ToString("x2"));
            return ptr;
        }
        internal static Func<string?, IntPtr> GetStringAllocater(bool globalAllocate)
        {
            if (globalAllocate)
            {
                switch (StringCharset)
                {
                    case InternalStringCharacterSet.ANSI:
                        return Marshal.StringToHGlobalAnsi;
                    case InternalStringCharacterSet.Unicode:
                        return Marshal.StringToHGlobalUni;
                    case InternalStringCharacterSet.Auto:
                        return Marshal.StringToHGlobalAuto;
                    default:
                        throw new NotImplementedException($"{typeof(Constants)}.{nameof(StringCharset)}={StringCharset} ({(int)StringCharset})");
                }
            }
            else
            {
                switch (StringCharset)
                {
                    case InternalStringCharacterSet.ANSI:
                        return Marshal.StringToCoTaskMemAnsi;
                    case InternalStringCharacterSet.Unicode:
                        return Marshal.StringToCoTaskMemUni;
                    case InternalStringCharacterSet.Auto:
                        return Marshal.StringToCoTaskMemAuto;
                    default:
                        throw new NotImplementedException($"{typeof(Constants)}.{nameof(StringCharset)}={StringCharset} ({(int)StringCharset})");
                }
            }
        }
        internal static IntPtr AllocateStringD(string[] value, out Action disposeAction, bool globalAllocate = true)
        {
            Func<string?, IntPtr> allocate = GetStringAllocater(globalAllocate);
            return AllocateStringD(value, allocate, out disposeAction);
        }
        internal static IntPtr AllocateStringD(string[] value, Func<string?, IntPtr> allocate, out Action disposeAction)
        {
            var log = LogManager.GetLogger("AllocateStringD");
            IntPtr ptr = IntPtr.Zero;
            lock (value)
            {
                if (value.Length > 0)
                {
                    var entries = new IntPtr[value.Length];
                    int byteSize = 0;
                    for (int i = 0; i < value.Length; i++)
                    {
                        byteSize += value[i].Length * sizeof(char);
                        entries[i] = allocate(value[i]);
                    }

                    ptr = Marshal.AllocHGlobal(byteSize);
                    Marshal.Copy(entries, 0, ptr, entries.Length);

                    disposeAction = () =>
                    {
                        Marshal.FreeHGlobal(ptr);
                        foreach (var item in entries)
                        {
                            Marshal.FreeHGlobal(item);
                        }
                    };
                }
                else
                {
                    disposeAction = () => { };
                }
            }
            return ptr;
        }

        /// <summary>
        /// Allocate a string in memory.
        /// </summary>
        /// <param name="value">Value for the string to be stored in memory.</param>
        /// <param name="disposeActionList">
        /// Reference to a list of actions that are called when the calling object is being disposed.
        /// <para>
        /// Will only be modified when this <b>does not</b> return <see cref="IntPtr.Zero"/>
        /// </para>
        /// </param>
        /// <exception cref="NotImplementedException">
        /// Thrown when there is not implementation to allocate & write a string with the charset defined in <see cref="StringCharset"/>
        /// </exception>
        /// <returns>
        /// Pointer to the string in memory.
        /// Will be <see cref="IntPtr.Zero"/> when <see langword="null"/> is used in the <paramref name="value"/> parameter.
        /// </returns>
        /// 
        /// <remarks>
        /// Depending on what <see cref="StringCharset"/> is set to, a different function will be called from <see cref="Marshal"/> to allocate a string.
        /// <list type="bullet">
        /// <item><see cref="InternalStringCharacterSet.ANSI"/> will use <see cref="Marshal.StringToHGlobalAnsi(string?)"/></item>
        /// </list>
        /// If <see cref="StringCharset"/> isn't set to any of the possible values listed above, then <see cref="NotImplementedException"/> will be thrown.
        /// </remarks>
        internal static IntPtr AllocateString(string? value, IList<Action> disposeActionList, bool globalAllocate = true)
        {
            var result = AllocateStringD(value, out var action, globalAllocate);
            if (result != IntPtr.Zero)
            {
                disposeActionList.Add(action);
            }
            return result;
        }

        /// <summary>
        /// Allocate a string array in memory as <c>const char**</c>. If you want it to be allocated as <c>char**</c>, then set <paramref name="globalAllocate"/> to <see langword="false"/>
        /// </summary>
        /// <param name="value">Array of strings</param>
        /// <param name="disposeActionList">List to insert the dispose action into</param>
        /// <param name="globalAllocate">When <see langword="true"/>, then the <c>AllocHGlobal</c> methods will be used. Otherwise <c>AllocCoTaskMem</c> methods will be used.</param>
        internal static IntPtr AllocateString(string[] value, IList<Action> disposeActionList, bool globalAllocate = true)
        {
            var result = AllocateStringD(value, out var action, globalAllocate);
            if (result != IntPtr.Zero)
            {
                disposeActionList.Add(action);
            }
            return result;
        }

        /// <summary>
        /// Allocate a list of delegates as <c>const void**</c>. If you want it to be allocated as <c>void**</c>, then set <paramref name="globalAllocate"/> to <see langword="false"/>
        /// </summary>
        /// <param name="value">Array of delegates</param>
        /// <param name="disposeAction">Action that should be called when disposing.</param>
        /// <param name="globalAllocate">Should <see cref="Marshal.AllocHGlobal(nint)"/> be used, or <see cref="Marshal.AllocCoTaskMem(int)"/>.</param>
        internal static IntPtr CreateArrayD(Delegate[] value, out Action disposeAction, bool globalAllocate = true)
        {
            return CreateArray(
                value,
                globalAllocate ? Marshal.AllocHGlobal : Marshal.AllocCoTaskMem,
                globalAllocate ? Marshal.FreeHGlobal : Marshal.FreeCoTaskMem,
                out disposeAction);
        }
        /// <summary>
        /// Allocate a list of delegates as <c>const void**</c>. If you want it to be allocated as <c>void**</c>, then set <paramref name="globalAllocate"/> to <see langword="false"/>
        /// </summary>
        /// <param name="value">Array of delegates</param>
        /// <param name="disposeActionList">List to insert the dispose action into</param>
        /// <param name="globalAllocate">Should <see cref="Marshal.AllocHGlobal(nint)"/> be used, or <see cref="Marshal.AllocCoTaskMem(int)"/>.</param>
        internal static IntPtr CreateArray(Delegate[] value, IList<Action> disposeActionList, bool globalAllocate = true)
        {
            var ptr = CreateArrayD(value, out var disposeAction, globalAllocate);
            disposeActionList.Add(disposeAction);
            return ptr;
        }
        internal static IntPtr CreateArray(Delegate[] value, Func<int, IntPtr> allocate, Action<IntPtr> free, out Action disposeAction)
        {
            IntPtr ptr = IntPtr.Zero;
            lock (value)
            {
                if (value.Length > 0)
                {
                    var entries = new IntPtr[value.Length];
                    int byteSize = 0;
                    for (int i = 0; i < value.Length; i++)
                    {
                        entries[i] = Marshal.GetFunctionPointerForDelegate(value[i]);
                        unsafe
                        {
                            byteSize += sizeof(nint);
                        }
                    }
                    ptr = allocate(byteSize);
                    Marshal.Copy(entries, 0, ptr, entries.Length);
                    disposeAction = () =>
                    {
                        free(ptr);
                    };
                }
                else
                {
                    disposeAction = () => { };
                }
            }
            return ptr;
        }
        private static Func<IntPtr, string?> GetStringReadFunction()
        {
            switch (StringCharset)
            {
                case InternalStringCharacterSet.ANSI:
                    return Marshal.PtrToStringAnsi;
                case InternalStringCharacterSet.Unicode:
                    return Marshal.PtrToStringUni;
                case InternalStringCharacterSet.Auto:
                    return Marshal.PtrToStringAuto;
            }
            throw new NotImplementedException($"{typeof(Constants)}.{nameof(StringCharset)}={StringCharset} ({(int)StringCharset})");
        }
        /// <summary>
        /// Read a <c>const char**</c> or <c>char**</c> to a string array.
        /// </summary>
        /// <param name="pointer"></param>
        /// <param name="length">Length of the array</param>
        /// <returns>Item will only be null when it's pointer is <see cref="IntPtr.Zero"/></returns>
        internal static string?[] ReadStringArray(IntPtr pointer, int length)
        {
            var ptrArray = new IntPtr[length];
            Marshal.Copy(pointer, ptrArray, 0, length);
            var result = new List<string?>();
            foreach (var item in ptrArray)
            {
                result.Add(GetStringReadFunction()(item));
            }
            return result.ToArray();
        }

        /// <summary>
        /// Read the string stored at a pointer.
        /// </summary>
        /// <param name="pointer">Pointer to the string.</param>
        /// <returns>
        /// <see langword="null"/> when <paramref name="pointer"/> is <see cref="IntPtr.Zero"/>. Otherwise string data will be returned.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// Thrown when there is not implementation to read a string with the charset defined in <see cref="StringCharset"/>
        /// </exception>
        /// 
        /// <remarks>
        /// Depending on what <see cref="StringCharset"/> is set to, a different function will be called from <see cref="Marshal"/> to read the string for that pointer.
        /// <list type="bullet">
        /// <item><see cref="InternalStringCharacterSet.ANSI"/> will use <see cref="Marshal.PtrToStringAnsi(nint)"/></item>
        /// </list>
        /// If <see cref="StringCharset"/> isn't set to any of the possible values listed above, then <see cref="NotImplementedException"/> will be thrown.
        /// </remarks>
        internal static string? ReadString(IntPtr pointer)
        {
            var log = LogManager.GetLogger("ReadString");
            log.Trace($"Reading " + pointer.ToString("x2"));
            if (pointer == IntPtr.Zero)
                return null;

            
            switch (StringCharset)
            {
                case InternalStringCharacterSet.ANSI:
                    return Marshal.PtrToStringAnsi(pointer);
                case InternalStringCharacterSet.Unicode:
                    return Marshal.PtrToStringUni(pointer);
                case InternalStringCharacterSet.Auto:
                    return Marshal.PtrToStringAuto(pointer);
            }

            throw new NotImplementedException($"{typeof(Constants)}.{nameof(StringCharset)}={StringCharset} ({(int)StringCharset})");
        }


        #region ParseEnum<T>(value, fallback)
        internal static T ParseEnum<T>(sbyte value, T fallback)
            where T : struct, Enum
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                return fallback;
            }
        }
        internal static T ParseEnum<T>(byte value, T fallback)
            where T : struct, Enum
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                return fallback;
            }
        }
        internal static T ParseEnum<T>(short value, T fallback)
            where T : struct, Enum
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                return fallback;
            }
        }
        internal static T ParseEnum<T>(int value, T fallback)
            where T : struct, Enum
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                return fallback;
            }
        }
        internal static T ParseEnum<T>(ushort value, T fallback)
            where T : struct, Enum
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                return fallback;
            }
        }
        internal static T ParseEnum<T>(uint value, T fallback)
            where T : struct, Enum
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                return fallback;
            }
        }
        internal static T ParseEnum<T>(long value, T fallback)
            where T : struct, Enum
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                return fallback;
            }
        }
        internal static T ParseEnum<T>(ulong value, T fallback)
            where T : struct, Enum
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                return fallback;
            }
        }
        #endregion
    }
}
