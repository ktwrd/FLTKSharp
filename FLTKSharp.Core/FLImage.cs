using System.Drawing;
using FLTKSharp.Core.Interfaces;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core
{
    public class FLImage : BaseFltkObject, IFltkImage
    {
        public static IFltkImage FromPointer(IntPtr ptr) => new FLImage(ptr);
        public IntPtr GetPointer() => Pointer;
        internal FLImage(IntPtr ptr)
            : base(ptr)
        { }

        public static FLImage FromDynamicPointer<T>(T obj)
            where T : BaseFltkObject
        {
            var ptr = Fl_Image_from_dyn_ptr(obj.Pointer);
            if (ptr == IntPtr.Zero)
                throw new InvalidCastException(
                    $"Cannot cast {typeof(T)} into {typeof(FLImage)} since {nameof(Fl_Image_from_dyn_ptr)} returned NULL");
            return new(ptr);
        }

        public void Draw(int x, int y, int width, int height)
        {
            Fl_Image_draw(Pointer, x, y, width, height);
        }

        public void Draw(int x, int y, int width, int height, int cx, int cy)
        {
            Fl_Image_draw_ext(Pointer, x, y, width, height, cx, cy);
        }
        public Size Size
        {
            get
            {
                var width = Fl_Image_width(Pointer);
                var height = Fl_Image_height(Pointer);
                return new(width, height);
            }
        }
        public virtual void Delete()
        {
            Fl_Image_delete(Pointer);
            Dispose();
        }
        public T Clone<T>() where T : IFltkImage
        {
            return (T)T.FromPointer(Fl_Image_copy(Pointer));
        }
        public T Clone<T>(Size size) where T : IFltkImage
        {
            return (T)T.FromPointer(Fl_Image_copy_sized(Pointer, size.Width, size.Height));
        }

        public void Scale(Size size, bool proportional, bool canExpand)
        {
            Fl_Image_scale(Pointer, size.Width, size.Height, proportional ? 1 : 0, canExpand ? 1 : 0);
        }

        public int Fail()
        {
            return Fl_Image_fail(Pointer);
        }

        public int Depth()
        {
            return Fl_Image_d(Pointer);
        }
    }
}
