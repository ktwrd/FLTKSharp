using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core
{
    public class FLImage : BaseFltkObject
    {
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

        public virtual FLImage Copy()
        {
            return new FLImage(Fl_Image_copy(Pointer));
        }
        public virtual FLImage Copy(int width, int height)
        {
            return new FLImage(Fl_Image_copy_sized(Pointer, width, height));
        }

        public virtual void Delete()
        {
            Fl_Image_delete(Pointer);
            Dispose();
        }
    }
}
