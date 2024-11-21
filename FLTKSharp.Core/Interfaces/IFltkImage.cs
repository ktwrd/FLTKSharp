using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLTKSharp.Core.Interfaces
{
    public interface IFltkImage
    {
        public abstract static IFltkImage FromPointer(IntPtr pointer);
        public abstract IntPtr GetPointer();

        /// <summary>
        /// <c>
        /// void Fl_Image_draw(image* self, int x, int y, int width, int height);
        /// </c>
        /// </summary>
        public void Draw(int x, int y, int width, int height);

        /// <summary>
        /// <c>
        /// void Fl_Image_draw_ext(image* self, int x, int y, int width, int height, int cx, int cy);
        /// </c>
        /// </summary>
        public void Draw(int x, int y, int width, int height, int cx, int cy);

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Image_width(image *);
        /// int Fl_Image_height(image *);
        /// </code>
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// <c>void Fl_Image_delete(image *);</c>
        /// </summary>
        public void Delete();

        /// <summary>
        /// <c>Fl_Image* Fl_Image_copy(image* self);</c>
        /// </summary>
        public T Clone<T>() where T : IFltkImage;

        /// <summary>
        /// <c>Fl_Image* Fl_Image_copy_sized(image* self, int width, int height);</c>
        /// </summary>
        public T Clone<T>(Size size) where T : IFltkImage;

        /// <summary>
        /// <c>void Fl_Image_scale(image* self, int width, int height, proportional, int can_expand);</c>
        /// </summary>
        public void Scale(Size size, bool proportional, bool canExpand);

        /// <summary>
        /// <c>int Fl_Image_fail(image* self);</c>
        /// </summary>
        public int Fail();

        /// <summary>
        /// <c>int Fl_Image_d(const image* self);</c>
        /// </summary>
        public int Depth();
    }
}
