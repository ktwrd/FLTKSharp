using System.Drawing;

namespace FLTKSharp.Core.Interfaces
{
    public interface IFltkInput : IFltkWidget
    {
        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// const char* Fl_Input_value(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// // Return value is boolean.
        /// // 0: Value not changed
        /// // Anything else: Value was changed!
        /// int Fl_Input_set_Value(widget* self, const char* text);
        /// </code>
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Input_maximum_size(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// void Fl_Input_set_maximum_size(widget* self, int value);
        /// </code>
        /// </summary>
        public int MaximumSize { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Input_position(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// // Will return non-zero value when position is different
        /// // to the one provided.
        /// int Fl_Input_set_position(widget* self, int position);
        /// </code>
        /// </summary>
        public int CursorPosition { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Input_mark(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// // Will return non-zero value when mark pos is different
        /// // to the one provided.
        /// int Fl_Input_set_mark(widget* self, int mark);
        /// </code>
        /// </summary>
        public int MarkPosition { get; set; }

        /// <summary>
        /// <code>
        /// int Fl_Input_replace(int b, int e, const char* text, int textLength);
        /// </code>
        /// </summary>
        /// <param name="start">Begining index of the text to be deleted</param>
        /// <param name="end">End index of the text to be deleted and insertion position</param>
        /// <param name="text">String that will be inserted</param>
        /// <returns>If anything was changed, then <see langword="true"/> will be returned.</returns>
        public bool ReplaceText(int start, int end, string text);

        /// <summary>
        /// Insert text at the current <see cref="CursorPosition"/>
        /// </summary>
        /// <returns>If anything was changed, then <see langword="true"/> will be returned.</returns>
        /// <remarks>
        /// <code>
        /// int Fl_Input_insert(widget* self, const char* text, int textLength);
        /// </code>
        /// </remarks>
        public bool InsertText(string text);

        /// <summary>
        /// Append text to the end.
        /// </summary>
        /// <param name="text">Text that will be appended</param>
        /// <param name="keepSelection">
        /// When set to <see langword="true"/>, then the current text that is selected will stay selected.
        /// Otherwise, the <see cref="CursorPosition"/> will move to the end of the inserted text.</param>
        /// <returns><see langword="false"/> if no text was appended.</returns>
        /// <remarks>
        /// <code>
        /// int Fl_Input_append(widget* self, const char* text, int textLength, char keep_selection=0);
        /// </code>
        /// </remarks>
        public bool Append(string text, bool keepSelection);

        /// <summary>
        /// Put the current selection into the clipboard
        /// </summary>
        /// <param name="clipboardDestination">Clipboard destination, <c>0</c> or <c>1</c></param>
        /// <returns>
        /// <see langword="true"/> when the selection was copied, 
        /// or <see langword="false"/> when no text was selected in the first place.
        /// </returns>
        /// <remarks>
        /// <code>
        /// int Fl_Input_copy(widget* self, int clipboard);
        /// </code>
        /// </remarks>
        public bool Copy(int clipboardDestination);

        /// <summary>
        /// Undo the previous changes to the text buffer.
        /// </summary>
        /// <returns><see langword="true"/> if any changes were made.</returns>
        /// <remarks>
        /// <code>
        /// int Fl_Input_undo(widget* self);
        /// </code>
        /// </remarks>
        public bool Undo();

        /// <summary>
        /// Copies the buffer to the clipboard
        /// </summary>
        /// <returns><see langword="false"/> if the operation did not change the clipboard</returns>
        /// <remarks>
        /// <code>
        /// int Fl_Input_copy_cuts(widget* self);
        /// </code>
        /// </remarks>
        public bool CopyCuts();

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// unsigned int Fl_Input_cursor_color(const widget* self);
        /// void Fl_get_color_rgb(unsigned int, unsigned char *r, unsigned char *g, unsigned char *b);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// unsigned int Fl_rgb_color(unsigned char r, unsigned char g, unsigned char b);
        /// void Fl_Input_set_cursor_color(widget* self, unsigned int color);
        /// </code>
        /// </summary>
        public Color CursorColor { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Input_text_font(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// void Fl_Input_set_text_font(widget* self, int value);
        /// </code>
        /// </summary>
        public int TextFont { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Input_text_size(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// void Fl_Input_set_text_size(widget* self, int size);
        /// </code>
        /// </summary>
        public int TextSize { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// // Non-zero is true
        /// int Fl_Input_readonly(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// void Fl_Input_set_readonly(widget* self, int boolean);
        /// </code>
        /// </summary>
        public bool ReadOnly { get; set; }


        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// // Non-zero is true
        /// int Fl_Input_wrap(widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// void Fl_Input_set_Wrap(widget* self, int boolean);
        /// </code>
        /// </summary>
        public bool WrapText { get; set; }

        /// <summary>
        /// <b>Getter</b>
        /// <code>
        /// int Fl_Input_tab_nav(const widget* self);
        /// </code>
        /// <b>Setter</b>
        /// <code>
        /// void Fl_Input_set_tab_nav(widget* self, int flag);
        /// </code>
        /// </summary>
        public int TabNavigationFlag { get; set; }
    }
}
