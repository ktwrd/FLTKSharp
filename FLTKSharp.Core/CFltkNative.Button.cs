using System.Runtime.InteropServices;

namespace FLTKSharp.Core
{
    internal partial class CFltkNative
    {
        /// <summary>
        /// Create a new button
        /// </summary>
        /// <param name="x">Absolute X position</param>
        /// <param name="y">Absolute Y position</param>
        /// <param name="width">Width of the button</param>
        /// <param name="height">Height of the button</param>
        /// <param name="labelPointer"><inheritdoc cref="Fl_Widget_new" path="/param[@name='labelPointer']"/></param>
        /// <returns></returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_new",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Fl_Button_new(int x, int y, int width, int height, IntPtr labelPointer);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_handle",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Button_handle(IntPtr button, FltkButtonHandleCallback callback, IntPtr data);

        /// <summary>
        /// Set the background color of the button.
        /// </summary>
        /// <param name="button"><inheritdoc cref="Fl_Button_new" path="/returns"/></param>
        /// <param name="color"></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_set_color",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Button_set_color(IntPtr button, int color);
    }
}
