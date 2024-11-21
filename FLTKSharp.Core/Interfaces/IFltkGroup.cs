using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLTKSharp.Core.Interfaces
{
    public interface IFltkGroup : IFltkWidget
    {
        /// <summary>
        /// <c>void Fl_Group_begin(widget *self);</c>
        /// </summary>
        public void Begin();
        /// <summary>
        /// <c>void Fl_Group_end(widget *self);</c>
        /// </summary>
        public void End();
        /// <summary>
        /// <c>int Fl_Group_find(widget *self, const void *);</c>
        /// </summary>
        /// <returns>Index of the child. Will be <c>-1</c> when not found.</returns>
        public int IndexOfChild(IFltkWidget child);
        /// <summary>
        /// <c>void Fl_Group_add(widget *Self, void *);</c>
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(IFltkWidget child);
        /// <summary>
        /// <c>void Fl_Group_insert(widget *self, void *, int position);</c>
        /// </summary>
        public void InsertChildAt(IFltkWidget child, int index);
        /// <summary>
        /// <c>void Fl_Group_remove(widget *self, void *child);</c>
        /// </summary>
        /// <param name="child"></param>
        public void RemoveChild(IFltkWidget child);
        /// <summary>
        /// <c>void Fl_Group_remove_by_index(widget *self, int index);</c>
        /// </summary>
        /// <param name="index"></param>
        public void RemoveChild(int index);
        /// <summary>
        /// <c>void Fl_Group_clear(widget *self);</c>
        /// </summary>
        public void RemoveAllChildren();
        /// <summary>
        /// <c>int Fl_Group_children(widget *self);</c>
        /// </summary>
        public int ChildCount();
        /// <summary>
        /// <c>Fl_Widget *Fl_Group_child(widget *self, int index);</c>
        /// </summary>
        /// <param name="index">Index of the child</param>
        /// <returns>Instance of the new widget, or null when doesn't exist.</returns>
        public object? ChildAt<T>(int index) where T : IFltkWidget;

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Group_clip_children(widget *self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// void Fl_Group_set_clip_children(widget *self, int value);
        /// </code>
        /// </summary>
        /// <remarks>
        /// Internally stored in FLTK as an integer.
        /// <see langword="false"/> should be <c>0</c>, and anything that isn't <c>0</c> should be parsed as <see langword="true"/>
        /// </remarks>
        public bool ClipChildren { get; set; }

        /// <summary>
        /// <c>void Fl_Group_init_sizes(widget *self);</c>
        /// </summary>
        public void InitializeSizes();

        /// <summary>
        /// <c>void Fl_Group_draw_child(const widget *self, Fl_Widget *w);</c>
        /// </summary>
        public void DrawChild(IFltkWidget child);

        /// <summary>
        /// <c>void Fl_Group_update_child(const widget *self, Fl_Widget *w);</c>
        /// </summary>
        public void UpdateChild(IFltkWidget child);

        /// <summary>
        /// <c>void Fl_Group_draw_outside_label(const widget *self, const Fl_Widget *w);</c>
        /// </summary>
        public void DrawOutsideLabel(IFltkWidget child);

        /// <summary>
        /// <c>void Fl_Group_draw_children(widget *self);</c>
        /// </summary>
        public void DrawChildren();
    }
}
