using System.Runtime.InteropServices;

namespace FLTKSharp.Core
{
    internal partial class CFltkNative
    {
        /// <summary>
        /// Create a new window.
        /// </summary>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="labelPointer"><inheritdoc cref="Fl_Widget_new" path="/param[@name='labelPointer']"/></param>
        /// <returns>Pointer to <c>Fl_Window</c></returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Window_new",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Fl_Window_new(int x, int y, int width, int height, IntPtr labelPointer);

        /// <summary>
        /// Finish creation of child widgets
        /// </summary>
        /// <param name="window"><inheritdoc cref="Fl_Window_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Window_end",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Window_end(IntPtr window);

        /// <summary>
        /// Show window
        /// </summary>
        /// <param name="window"><inheritdoc cref="Fl_Window_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Window_show",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Window_show(IntPtr window);

        /// <summary>
        /// Hide window
        /// </summary>
        /// <param name="window"><inheritdoc cref="Fl_Window_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Window_hide",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Window_hide(IntPtr window);

        /// <summary>
        /// Get the height of the <paramref name="window"/> provided.
        /// </summary>
        /// <param name="window"><inheritdoc cref="Fl_Window_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Window_height",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Window_height(IntPtr window);
    }
}
