using System.Runtime.InteropServices;

namespace FLTKSharp.Core
{
    internal partial class CFltkNative
    {
        /// <returns>Pointer to <c>Fl_Image</c></returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_new",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Fl_Image_new(int width, int height, int depth);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_copy",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Fl_Image_copy(IntPtr image);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_copy_sized",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Fl_Image_copy_sized(IntPtr image, int width, int height);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_count",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Image_count(IntPtr image);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_d",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Image_depth(IntPtr image);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_data",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Fl_Image_data(IntPtr image);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_fail",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Image_fail(IntPtr image);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_draw",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Image_draw(IntPtr image, int x, int y, int width, int height);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_draw_ext",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Image_draw_ext(IntPtr image, int x, int y, int width, int height, int cx, int cy);


        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_width",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Image_width(IntPtr image);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_height",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Image_height(IntPtr image);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_set_scaling_algorithm",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Image_set_scaling_algorithm(IntPtr image, int value);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_scaling_algorithm",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Image_scaling_algorithm(IntPtr image);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Image_scaling_algorithm",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Image_delete(IntPtr image);
    }
}
