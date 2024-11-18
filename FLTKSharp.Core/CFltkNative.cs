﻿using System.Runtime.InteropServices;

namespace FLTKSharp.Core
{
    internal partial class CFltkNative
    {
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_init_all",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_init_all();
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_lock",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_lock();

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Widget_delete",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Widget_delete(IntPtr widget);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_redraw",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Group_redraw(IntPtr group);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_run",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_run();
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_awake",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_awake();

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_unlock",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_unlock();

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_load_system_icons",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_load_system_icons();

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_set_contrast_level",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_set_contrast_level(int level);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_contrast_level",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_contrast_level();

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_using_wayland",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_using_wayland();
    }
}
