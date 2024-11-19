using System.Runtime.InteropServices;
using static FLTKSharp.Core.Constants;

namespace FLTKSharp.Core
{
    internal partial class InternalHelper
    {
        private static IntPtr AllocateString(string? value, out Action disposeAction)
        {
            IntPtr ptr = IntPtr.Zero;
            if (value != null)
            {
                switch (StringCharset)
                {
                    case InternalStringCharacterSet.ANSI:

                        ptr = Marshal.StringToHGlobalAnsi(value);
                        break;
                    default:
                        throw new NotImplementedException($"{typeof(Constants)}.{nameof(StringCharset)}={StringCharset} ({(int)StringCharset})");
                }
                disposeAction = () => Marshal.FreeHGlobal(ptr);
            }
            else
            {
                disposeAction = () => { };
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
        internal static IntPtr AllocateString(string? value, IList<Action> disposeActionList)
        {
            var result = AllocateString(value, out var action);
            if (result != IntPtr.Zero)
            {
                disposeActionList.Add(action);
            }
            return result;
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
            if (pointer == IntPtr.Zero)
                return null;

            switch (StringCharset)
            {
                case InternalStringCharacterSet.ANSI:
                    return Marshal.PtrToStringAnsi(pointer);
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
