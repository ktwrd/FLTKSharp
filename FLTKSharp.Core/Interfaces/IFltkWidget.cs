using System.Drawing;

namespace FLTKSharp.Core.Interfaces
{
    public interface IFltkWidget
    {
        public abstract static IFltkWidget FromPointer(IntPtr pointer);
        public abstract IntPtr GetPointer();

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Widget_x(widget *);
        /// int Fl_Widget_y(widget *);
        /// </code>
        /// <b>Setter</b>
        /// <code>void Fl_Widget_resize(widget *, int x, int y, int width, int height);</code>
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Widget_width(widget *);
        /// int Fl_Widget_height(widget *);
        /// </code>
        /// <b>Setter</b>
        /// <code>void Fl_Widget_resize(widget *, int x, int y, int width, int height);</code>
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>const char* Fl_Widget_label(widget *);</code>
        /// <b>Setter</b>
        /// <code>void Fl_Widget_set_label(widget *, const char *title);</code>
        /// </summary>
        /// <remarks>
        /// Use <see cref="InternalHelper.AllocateString(string?, IList{Action})"/> for allocation,
        /// and <see cref="InternalHelper.ReadString(nint)"/> for reading it.
        /// </remarks>
        public string? LabelText { get; set; }

        /// <summary>
        /// <c>void Fl_Widget_hide(widget *);</c>
        /// </summary>
        public void Show();

        /// <summary>
        /// <c>void Fl_Widget_show(widget *);</c>
        /// </summary>
        public void Hide();

        /// <summary>
        /// <b>Getter</b>
        /// <code>int Fl_Widget_active(widget *);</code>
        /// <b>Setter</b>
        /// <code>
        /// // Check if active
        /// int Fl_Widget_active(widget *);
        /// // Enable widget
        /// void Fl_Widget_activate(widget *);
        /// // Disable widget
        /// void Fl_Widget_deactivate(widget *);
        /// </code>
        /// </summary>
        /// <example>
        /// Example getter and setter
        /// <code>
        /// get => Fl_Widget_active(Pointer) != 0;
        /// set => value ? Fl_Widget_activate(Pointer) : Fl_Widget_deactivate(Pointer);
        /// </code>
        /// </example>
        public bool Enabled { get; set; }

        /// <summary>
        /// <c>void Fl_Widget_redraw(widget *);</c>
        /// </summary>
        public void Redraw();
        /// <summary>
        /// <c>void Fl_Widget_redraw_label(widget *);</c>
        /// </summary>
        public void RedrawLabel();

        /// <summary>
        /// <c>void Fl_Widget_resize(widget *, int x, int y, int width, int height);</c>
        /// </summary>
        public void Resize(int x, int y, int width, int height);
        /// <summary>
        /// Resize this widget.
        /// </summary>
        /// <remarks>
        /// Should internally call <see cref="Resize(int, int, int, int)"/>
        /// </remarks>
        public void Resize(Point position, Size size);

        /// <summary>
        /// <b>Getter</b>
        /// <code>const char* Fl_Widget_tooltip(widget *);</code>
        /// <b>Setter</b>
        /// <code>void Fl_Widget_set_tooltip(widget *, const char *txt);</code>
        /// </summary>
        /// <remarks>
        /// Use <see cref="InternalHelper.AllocateString(string?, IList{Action})"/> for allocation,
        /// and <see cref="InternalHelper.ReadString(nint)"/> for reading it.
        /// </remarks>
        public string? TooltipText { get; set; }

        public int LabelFont { get; set; }
        public int LabelSize { get; set; }
        public FltkLabelType LabelType { get; set; }
    }
}
