using System.Runtime.InteropServices;

namespace FLTKSharp.Core
{
    internal partial class CFltkNative
    {
        /// <summary>
        /// <para><b>Creates a widget at the given position and size.</b></para>
        /// 
        /// <para>The Fl_Widget is a protected constructor, but all derived widgets have a
        /// matching public constructor. It takes a value for x(), y(), w(), h(), and
        /// an optional value for <see cref="Fl_Widget_label"/>.</para>
        /// </summary>
        /// <param name="x">the position of the widget relative to the enclosing window</param>
        /// <param name="y">the position of the widget relative to the enclosing window</param>
        /// <param name="width">size of the widget in pixels</param>
        /// <param name="heigt">size of the widget in pixels</param>
        /// <param name="labelPointer">Pointer to a string made with <see cref="Marshal.StringToHGlobalAnsi"/> (<see cref="IntPtr.Zero"/> when <see langword="null"/>)</param>
        /// <returns>Pointer to <c>Fl_Widget</c></returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_new",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Fl_Widget_new([In] int x, [In] int y, [In] int width, [In] int heigt, IntPtr labelPointer);

        /// <summary>
        /// <para><b>Sets the box type for the widget.</b></para>
        /// 
        /// This identifies a routine that draws the background of the widget.
        /// See <see cref="FltkBoxType"/> for the available types. The default depends on the
        /// widget, but is usually <see cref="FltkBoxType.NoBox"/> or <see cref="FltkBoxType.UpBox"/>.
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <param name="boxType">Value from <see cref="FltkBoxType"/> casted to <see cref="int"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_set_box",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Widget_set_box(IntPtr widget, int boxType);

        /// <summary>
        /// Gets the current label text.
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <returns>Pointer to a string made with <see cref="Marshal.StringToHGlobalAnsi"/> (<see cref="IntPtr.Zero"/> when <see langword="null"/>)</returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_label",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Fl_Widget_label(IntPtr widget);

        /// <summary>
        /// <para><b>Sets the current label pointer.</b></para>
        /// 
        /// The label is shown somewhere on or next to the widget.
        /// 
        /// The passed pointer is stored unchanged in the widget (the string is \em not copied), so if
        /// you need to set the label to a formatted value, make sure the buffer is
        /// static, global, or allocated.
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <param name="labelPointer">Pointer to a string made with <see cref="Marshal.StringToHGlobalAnsi"/> (<see cref="IntPtr.Zero"/> when <see langword="null"/>)</param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_set_label",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Widget_set_label(IntPtr widget, IntPtr labelPointer);

        /// <summary>
        /// <b>Gets the font size in pixels.</b>
        /// <para>The default size is 14 pixels.</para>
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <returns>The current font size</returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_label_size",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Widget_label_size(IntPtr widget);

        /// <summary>
        /// <para><b>Sets the font size in pixels.</b></para>
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <param name="size">New font size</param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_set_label_size",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Widget_set_label_size(IntPtr widget, [In] int size);

        /// <summary>
        /// Gets the label type.
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <returns>The current label type (<see cref="FltkLabelType"/>)</returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_label_type",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Widget_label_type(IntPtr widget);

        /// <summary>
        /// <para><b>Sets the label type.</b></para>
        /// 
        /// The label type identifies the function that draws the label of the widget.
        /// This is generally used for special effects such as embossing or for using
        /// the label() pointer as another form of data such as an icon. The value
        /// <see cref="FltkLabelType.Normal"/> prints the label as plain text.
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <param name="value">See <see cref="FltkLabelType"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_set_label_type",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Widget_set_label_type(IntPtr widget, [In] int value);

        /// <summary>
        /// Get the label font for a widget
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <returns>Value in <see cref="FltkFont"/> or index of the font in the internal font table.</returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_set_label_type",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Widget_label_font(IntPtr widget);

        /// <summary>
        /// <para><b>Makes a widget visible.</b></para>
        /// 
        /// <para>
        /// An invisible widget never gets redrawn and does not get keyboard
        /// or mouse events, but can receive a few other events like <see cref="FltkEvent.Show"/>.</para>
        /// 
        /// <para>
        /// The <see cref="Fl_Widget_visible"/> method returns true if the widget is set to be
        /// visible. The visible_r() method returns true if the widget and
        /// all of its parents are visible. A widget is only visible if
        /// visible() is true on it <I>and all of its parents</I>.</para>
        /// 
        /// <para>
        /// Changing it will send <see cref="FltkEvent.Show"/> or <see cref="FltkEvent.Hide"/> events to the widget.
        /// <I>Do not change it if the parent is not visible, as this
        /// will send false <see cref="FltkEvent.Show"/> or <see cref="FltkEvent.Hide"/> events to the widget</I>.
        /// <see cref="Fl_Widget_redraw"/>() is called if necessary on this or the parent.</para>
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_set_label_type",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Widget_show(IntPtr widget);

        /// <summary>
        /// <para><b>Schedules the drawing of the widget.</b></para>
        /// 
        /// Marks the widget as needing its <see cref="Fl_Widget_draw"/> routine called.
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_redraw",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Widget_redraw(IntPtr widget);

        /// <summary>
        /// <para><b>Draws the widget.</b></para>
        /// 
        /// <para>
        /// Never call this function directly. FLTK will schedule redrawing whenever
        /// needed. If your widget must be redrawn as soon as possible, call <see cref="Fl_Widget_redraw"/>
        /// instead.</para>
        /// 
        /// <para>
        /// Override this function to draw your own widgets.</para>
        /// 
        /// <para>
        /// If you ever need to call another widget's draw method <I>from within your
        /// own draw() method</I>, e.g. for an embedded scrollbar.</para>
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_draw",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Widget_draw(IntPtr widget);

        /// <summary>
        /// Returns whether a widget is visible.
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <returns><see langword="false"/> if the widget is not drawn and hence invisible.</returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_visible",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool Fl_Widget_visible(IntPtr widget);

        /// <summary>
        /// <para><b>Returns whether a widget and all its parents are visible.</b></para>
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <returns><see langword="false"/> if the widget or any of its parents are invisible.</returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_visible_r",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool Fl_Widget_visible_r(IntPtr widget);

        /// <summary>
        /// Makes a widget invisible.
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_set_label_type",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Widget_hide(IntPtr widget);

        /// <summary>
        /// <para><b>Returns a pointer to the parent widget.</b></para>
        /// Usually this is a Fl_Group or Fl_Window.
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <returns><see cref="IntPtr.Zero"/> if the widget has no parent</returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_parent",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Fl_Widget_parent(IntPtr widget);

        /// <summary>
        /// <para><b>Gives the widget the keyboard focus.</b></para>
        /// 
        /// Tries to make this widget be the <c>Fl::focus()</c> widget, by first sending
        /// it an <see cref="FltkEvent.Focus"/> event, and if it returns non-zero, setting
        /// <c>Fl::focus()</c> to this widget. You should use this method to
        /// assign the focus to a widget.
        /// </summary>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <returns><see langword="true"/> if the widget accepted the focus</returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_take_focus",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool Fl_Widget_take_focus(IntPtr widget);
    }
}
