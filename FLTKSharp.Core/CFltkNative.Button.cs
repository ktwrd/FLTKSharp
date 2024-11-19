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
        internal static extern void Fl_Button_handle(IntPtr button, FltkWidgetHandleCallback callback, IntPtr data);

        /// <summary>
        /// Set the background color of the button.
        /// </summary>
        /// <param name="button"><inheritdoc cref="Fl_Button_new" path="/returns"/></param>
        /// <param name="color">TODO how the fuck does this work? hex works (as RGBA), but also lookup numbers</param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_set_color",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Button_set_color(IntPtr button, int color);

        /// <param name="button"><inheritdoc cref="Fl_Button_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_set_color",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Button_clear(IntPtr button);

        /// <param name="button"><inheritdoc cref="Fl_Button_new" path="/returns"/></param>
        /// <returns><c>1</c> should be <see langword="true"/>, and <c>0</c> should be <see langword="false"/></returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_compact",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern char Fl_Button_compact(IntPtr button);

        /// <param name="button"><inheritdoc cref="Fl_Button_new" path="/returns"/></param>
        /// <param name="value"><c>1</c> for <see langword="true"/>, <c>0</c> for <see langword="false"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_set_compact",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Button_set_compact(IntPtr button, char value);

        /// <param name="button"><inheritdoc cref="Fl_Button_new" path="/returns"/></param>
        /// <returns>Value should be an int that can be turned into <see cref="FltkBoxType"/></returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_down_box",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Button_down_box(IntPtr button);
        /// <param name="button"><inheritdoc cref="Fl_Button_new" path="/returns"/></param>
        /// <param name="value">Value should be <see cref="FltkBoxType"/> that is turned into an int</param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_set_down_box",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Button_set_down_box(IntPtr button, int value);
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_redraw",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Button_redraw(IntPtr button);
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_shortcut",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Button_shortcut(IntPtr button);
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Button_set_shortcut",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Button_set_shortcut(IntPtr button, int value);
    }
}
