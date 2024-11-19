using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core
{
    public class FLImage : BaseFltkObject
    {
        public FLImage(int width, int height, int depth)
            : this(Fl_Image_new(width, height, depth))
        { }

        internal FLImage(IntPtr ptr)
            : base(ptr)
        { }

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
