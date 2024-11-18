using System.Runtime.InteropServices;

namespace FLTKSharp.Core
{
    internal partial class CFltkNative
    {
        /// <summary>
        /// Create a new group, which is a container that can hold widgets 'n such.
        /// </summary>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="labelPointer"><inheritdoc cref="Fl_Widget_new" path="/param[@name='labelPointer']"/></param>
        /// <returns>Pointer to <c>Fl_Group</c></returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_new",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Fl_Group_new(int x, int y, int width, int height, IntPtr labelPointer);

        /// <summary>
        /// Add a child to this group
        /// </summary>
        /// <param name="group"><inheritdoc cref="Fl_Group_new" path="/returns"/></param>
        /// <param name="widget">Widget to add as a child</param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_add",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Group_add(IntPtr group, IntPtr widget);

        /// <summary>
        /// Remove a child widget by it's pointer
        /// </summary>
        /// <param name="group"><inheritdoc cref="Fl_Group_new" path="/returns"/></param>
        /// <param name="widget">Pointer to the child widget</param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_remove",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Group_remove(IntPtr group, IntPtr widget);

        /// <summary>
        /// Remove a child widget by it's index.
        /// </summary>
        /// <param name="group"><inheritdoc cref="Fl_Group_new" path="/returns"/></param>
        /// <param name="index">Index of the child to remove</param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_remove_by_index",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Group_remove_by_index(IntPtr group, int index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"><inheritdoc cref="Fl_Group_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_begin",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Group_begin(IntPtr group);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"><inheritdoc cref="Fl_Group_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_end",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Group_end(IntPtr group);

        /// <summary>
        /// Returns how many child widgets the group has.
        /// </summary>
        /// <param name="group"><inheritdoc cref="Fl_Group_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_children",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Group_children(IntPtr group);

        /// <summary>
        /// Get a child of this group at a specific index.
        /// </summary>
        /// <param name="group"><inheritdoc cref="Fl_Group_new" path="/returns"/></param>
        /// <param name="index">Index of the child</param>
        /// <returns><see cref="IntPtr.Zero"/> when index is out of bounds.</returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_child",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Fl_Group_child(IntPtr group, int index);

        /// <summary>
        /// Try and find the index of the <paramref name="childWidget"/> provided.
        /// </summary>
        /// <param name="group"><inheritdoc cref="Fl_Group_new" path="/returns"/></param>
        /// <param name="childWidget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <returns>Index of the child. Will return the result of <see cref="Fl_Group_children"/> when it could not be found.</returns>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_find",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int Fl_Group_find(IntPtr group, IntPtr childWidget);

        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_resize",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Group_resize(IntPtr group, int x, int y, int width, int height);

        /// <summary>
        /// Insert a widget at a specific index, like <see cref="List{T}.Insert(int, T)"/>
        /// </summary>
        /// <param name="group"><inheritdoc cref="Fl_Group_new" path="/returns"/></param>
        /// <param name="widget"><inheritdoc cref="Fl_Widget_new" path="/returns"/></param>
        /// <param name="index">Index to insert the child at</param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_insert",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Group_insert(IntPtr group, IntPtr widget, int index);

        /// <summary>
        /// <para><b>Deletes all child widgets from memory recursively.</b></para>
        /// 
        /// This method differs from the <see cref="Fl_Group_remove"/> method in that it
        /// affects all child widgets and deletes them from memory.
        /// </summary>
        /// <param name="group"><inheritdoc cref="Fl_Group_new" path="/returns"/></param>
        [DllImport(Constants.LibraryFilename,
            EntryPoint = "Fl_Group_clear",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void Fl_Group_clear(IntPtr group);
    }
}
