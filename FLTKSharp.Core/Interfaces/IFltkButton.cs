using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLTKSharp.Core.Interfaces
{
    public interface IFltkButton : IFltkWidget
    {
        /// <summary>
        /// <b>Getter</b>
        /// <code>int Fl_Button_shortcut(const widget *self);</code>
        /// <b>Setter</b>
        /// <code>void Fl_Button_set_shortcut(const widget *self, int shortcut);</code>
        /// </summary>
        public int Shortcut { get; set; }

        /// <summary>
        /// <c>int Fl_Button_clear(widget *self);</c>
        /// </summary>
        public int Clear();

        /// <summary>
        /// <b>Getter</b>
        /// <code>int Fl_Button_value(const widget *self);</code>
        /// <b>Setter</b>
        /// <code>void Fl_Button_set_value(const widget *self, int flag);</code>
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>int Fl_Button_down_box(const widget *self);</code>
        /// <b>Setter</b>
        /// <code>void Fl_Button_set_down_box(const widget *self, int flag);</code>
        /// </summary>
        public FltkBoxType PressBoxType { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>unsigned char Fl_Button_compact(widget *self);</code>
        /// <b>Setter</b>
        /// <code>void Fl_Button_set_compact(widget *self, unsigned char value);</code>
        /// </summary>
        /// <remarks>
        /// Value for FLTK should be parsed as a character!!!
        /// Where the <c>1</c> character means <see langword="true"/>, and anything else should be assumed to be <see langword="false"/>
        /// </remarks>
        public bool Compact { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// void Fl_get_color_rgb(unsigned int, unsigned char *r, unsigned char *g, unsigned char *b);
        /// unsigned int Fl_button_color(widget *);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// unsigned int Fl_rgb_color(unsigned char r, unsigned char g, unsigned char b);
        /// void Fl_Button_set_color(widget *, unsigned int);
        /// </code>
        /// </summary>
        /// <remarks>For an example, <see cref="FLButton.BackgroundColor"/></remarks>
        public Color BackgroundColor { get; set; }
    }
}
