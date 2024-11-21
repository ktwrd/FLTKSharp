using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLTKSharp.Core.Interfaces
{
    public interface IFltkMenu 
    {
        /// <summary>
        /// <c>void Fl_Menu_Bar_add(widget* self, const char* name, int shortcut, Fl_Callback* cb, void* data, int flag);</c>
        /// </summary>
        public void Add(string name, int shortcut, FltkWidgetCallback callback, int flag);

        /// <summary>
        /// <c>void Fl_Menu_Bar_insert(widget* self, int index, const char* name, int shortcut, Fl_Callback* cb, void* data, int flag);</c>
        /// </summary>
        public void Insert(int index, string name, int shortcut, FltkWidgetCallback callback, int flag);

        /// <summary>
        /// <c>Fl_Menu_Item* Fl_Menu_Bar_get_item(widget* self, const char* name);</c>
        /// </summary>
        public FLMenuItem? GetItem(string name);

        /// <summary>
        /// <c>int Fl_Menu_Bar_set_item(widget* self, Fl_Menu_Item* item);</c>
        /// </summary>
        public int SetItem(FLMenuItem item);

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Menu_Bar_text_font(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// void Fl_Menu_Bar_set_text_font(widget* self, int font);
        /// </code>
        /// </summary>
        public int TextFont { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Menu_Bar_text_size(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// void Fl_Menu_Bar_set_text_size(widget* self, int size);
        /// </code>
        /// </summary>
        public int TextSize { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// unsigned int Fl_Menu_Bar_text_color(widget *);
        /// void Fl_get_color_rgb(unsigned int, unsigned char *r, unsigned char *g, unsigned char *b);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// unsigned int Fl_rgb_color(unsigned char r, unsigned char g, unsigned char b);
        /// void Fl_Menu_Bar_set_text_color(widget *, unsigned int);
        /// </code>
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// <c>int Fl_Menu_Bar_add_choice(widget* self, const char* str);</c>
        /// </summary>
        public int AddChoice(string value);

        /// <summary>
        /// <c>const char* Fl_Menu_Bar_get_choice(widget* self);</c>
        /// </summary>
        public string? GetChoice();

        /// <summary>
        /// <c>int Fl_Menu_Bar_value(widget* self);</c>
        /// </summary>
        public int GetValue();

        /// <summary>
        /// <c>int Fl_Menu_Bar_set_value(widget* self, int v);</c>
        /// </summary>
        public int SetValue(int v);

        /// <summary>
        /// <c>void Fl_Menu_Bar_clear(widget* self);</c>
        /// </summary>
        public void Clear();

        /// <summary>
        /// <c>int Fl_Menu_Bar_clear_submenu(widget* self, int index);</c>
        /// </summary>
        public int ClearSubmenu(int index);

        /// <summary>
        /// <c>int Fl_Menu_Bar_size(const widget* self);</c>
        /// </summary>
        public int Size();

        /// <summary>
        /// <c>const char* Fl_Menu_Bar_text(const widget* self, int index);</c>
        /// </summary>
        public string? Text(string index);

        /// <summary>
        /// <c>const Fl_Menu_Item* Fl_Menu_Bar_at(const widget* self, int index);</c>
        /// </summary>
        public FLMenuItem MenuItemAt(int index);

        /// <summary>
        /// <c>void Fl_Menu_Bar_set_mode(widget* self, int i, int fl);</c>
        /// </summary>
        public void SetMode(int index, int mode);
        /// <summary>
        /// <c>int Fl_Menu_Bar_mode(const widget* self, int i);</c>
        /// </summary>
        public int GetMode(int index);

        /// <summary>
        /// <c>int Fl_Menu_Bar_find_index(const widget* self, const char* label);</c>
        /// </summary>
        public int FindIndex(string label);

        /// <summary>
        /// <c>const Fl_Menu_Item* Fl_Menu_Bar_menu(const widget* self);</c>
        /// </summary>
        /// <returns></returns>
        public FLMenuItem Menu();

        /// <summary>
        /// <c>void Fl_Menu_Bar_set_menu(widget* self, const Fl_Menu_Item* item);</c>
        /// </summary>
        public void SetMenu(FLMenuItem menuItem);

        /// <summary>
        /// <c>void Fl_Menu_Bar_remove(widget* self, int index);</c>
        /// </summary>
        public void Remove(int index);

        /// <summary>
        /// <c>void Fl_Menu_Bar_set_down_box(widget* self, int box);</c>
        /// </summary>
        public void SetDownBox(FltkBoxType type);

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Menu_Bar_down_box(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// void Fl_Menu_Bar_set_down_box(widget* self, int box);
        /// </code>
        /// </summary>
        public FltkBoxType DownBox { get; set; }

        /// <summary>
        /// <c>void Fl_Menu_Bar_global(widget* self);</c>
        /// </summary>
        public void Global();

        /// <summary>
        /// <c>int Fl_Menu_Bar_item_pathname(const widget* self, char* pathname, int pathnamelen, const Fl_Menu_Item* item);</c>
        /// </summary>
        public int ItemPathname(string pathName, FLMenuItem menuItem);

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Menu_Bar_menu_box(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// void Fl_Menu_Bar_set_menu_box(widget* self, int box);
        /// </code>
        /// </summary>
        public FltkBoxType MenuBox { get; set; }

        /// <summary>
        /// <c>Fl_Menu_Item* Fl_Menu_Bar_mvalue(const widget *self);</c>
        /// </summary>
        public FLMenuItem? MenuValue();

        /// <summary>
        /// <c>Fl_Menu_Item* Fl_Menu_Bar_prev_mvalue(const widget *self);</c>
        /// </summary>
        /// <returns></returns>
        public FLMenuItem? PreviousMenuValue();
    }
}
