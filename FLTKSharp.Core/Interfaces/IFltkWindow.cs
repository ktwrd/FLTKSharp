using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLTKSharp.Core.Interfaces
{
    public interface IFltkWindow : IFltkGroup
    {
        /// <summary>
        /// <c>void Fl_Window_make_modal(Fl_Window* window, unsigned int boolean);</c>
        /// </summary>
        public void MakeModal(bool state);

        /// <summary>
        /// <c>void Fl_Window_fullscreen(Fl_Window* window, unsigned int boolean);</c>
        /// </summary>
        public void Fullscreen(bool state);

        /// <summary>
        /// <c>void Fl_Window_make_current(Fl_Window* window);</c>
        /// </summary>
        public void MakeCurrent();
        
        /// <summary>
        /// Getter
        /// <code>
        /// void* Fl_Window_icon(const Fl_Window* widget);
        /// </code>
        /// Setter
        /// <code>
        /// void Fl_Window_set_icon(const Fl_Window* widget, void* icon);
        /// </code>
        /// </summary>
        public IntPtr Icon { get; set; }
        
        /// <summary>
        /// <c>int Fl_Window_shown(Fl_Window* self);</c>
        /// </summary>
        public bool Visible { get; }
        
        /// <summary>
        /// Getter
        /// <code>void Fl_Window_set_border(const Fl_Window* self, int flag);</code>
        /// Setter
        /// <code>int Fl_Window_border(const Fl_Window* self);</code>
        /// </summary>
        public int Border { get; set; }
        
        /// <summary>
        /// Getter
        /// <code>void* Fl_Window_region(const Fl_Window* self);</code>
        /// Setter
        /// <code>void Fl_Window_set_region(const Fl_Window* self, void* region);</code>
        /// </summary>
        /// <remarks>Pointer to <c>Fl_X</c> (i have no idea what it does)</remarks>
        public IntPtr Region { get; set; }

        /// <summary>
        /// <c>void Fl_Window_iconize(Fl_Window* self);</c>
        /// </summary>
        public void Iconize();
        
        /// <summary>
        /// Getter
        /// <code>
        /// unsigned int Fl_Window_fullscreen_active(const Fl_Window* self);
        /// </code>
        /// </summary>
        public bool FullscreenActive { get; }

        /// <summary>
        /// <c>void Fl_Window_free_position(Fl_Window* self);</c>
        /// </summary>
        public void FreePosition();
        
        /// <summary>
        /// Getter
        /// <code>
        /// int Fl_Window_decorated_w(const Fl_Window* self);
        /// int Fl_Window_decorated_h(const Fl_Window* self);
        /// </code>
        /// </summary>
        public Size DecoratedSize { get; }

        /// <summary>
        /// <c>
        /// void Fl_Window_size_range(Fl_Window* self, int minw, int minh, int maxw, int maxh);
        /// </c>
        /// </summary>
        public void SetSizeRange(Size min, Size max);

        /// <summary>
        /// <c>void Fl_Window_hotspot(Fl_Window* self, Fl_Widget* target);</c>
        /// </summary>
        public void SetHotspot(IFltkWidget child);
        
        public IFltkImage? Shape { get; set; }
        
        /// <summary>
        /// Getter
        /// <code>
        /// int Fl_Window_x_root(const Fl_Window* self);
        /// int Fl_Window_y_root(const Fl_Window* self);
        /// </code>
        /// </summary>
        public Point RootPosition { get; }

        /// <summary>
        /// <c>
        /// void Fl_Window_set_cursor_image(Fl_Window* self, const Fl_RGB_Image* image, int hot_x, int hot_y);
        /// </c>
        /// </summary>
        public void SetCursorImage(IFltkRgbImage? image, Point hot);

        /// <summary>
        /// <c>void Fl_Window_default_cursor(Fl_Window* self, int cursor);</c>
        /// </summary>
        /// <param name="kind">Cast into <see cref="int"/></param>
        public void DefaultCursor(FltkCursorKind kind);
        
        /// <summary>
        /// Getter
        /// <code>int Fl_Window_screen_num(Fl_Window* self);</code>
        /// Setter
        /// <code>void Fl_Window_set_screen_num(Fl_Window* self, int screen);</code>
        /// </summary>
        public int ScreenNumber { get; set; }

        /// <summary>
        /// <c>void Fl_Window_wait_for_expose(Fl_Window* self);</c>
        /// </summary>
        public void WaitForExpose();
        
        /// <summary>
        /// Getter
        /// <code>unsigned char Fl_Window_alpha(const Fl_Window* self);</code>
        /// Setter
        /// <code>void Fl_Window_set_alpha(const Fl_Window* self, unsigned char value);</code>
        /// </summary>
        public byte Alpha { get; set; }

        /// <summary>
        /// <c>void Fl_Window_force_position(Fl_Window* self, int flag);</c>
        /// </summary>
        public void ForcePosition(int flag);
        
        /// <summary>
        /// Getter
        /// <code>const char *s Fl_Window_default_xclass(Fl_Window* self);</code>
        /// Setter
        /// <code>void Fl_Window_set_default_xclass(Fl_Window* self, const char *s);</code>
        /// </summary>
        public static string? DefaultXClass { get; set; }
        
        /// <summary>
        /// Getter
        /// <code>const char *s Fl_Window_xclass(Fl_Window* self);</code>
        /// Setter
        /// <code>void Fl_Window_set_xclass(Fl_Window* self, const char *s);</code>
        /// </summary>
        public string? XClass { get; set; }

        /// <summary>
        /// <c>void Fl_Window_clear_modal_states(Fl_Window* self);</c>
        /// </summary>
        public void ClearModalStates();
        
        /// <summary>
        /// Getter
        /// <code>const char* Fl_Window_icon_label(const Fl_Window* self);</code>
        /// Setter
        /// <code>void Fl_Window_set_icon_label(const Fl_Window* self, const char* label);</code>
        /// </summary>
        public string? IconLabel { get; set; }

        /// <summary>
        /// <c>void Fl_Window_set_icons(Fl_Window* self, const Fl_RGB_Image* images[], int length);</c>
        /// </summary>
        public void SetIcons<T>(List<T> images) where T : IFltkRgbImage;

        /// <summary>
        /// <c>void Fl_Window_maximize(Fl_Window* self);</c>
        /// </summary>
        public void Maximize();
        
        /// <summary>
        /// <c>void Fl_Window_minimize(Fl_Window* self);</c>
        /// </summary>
        public void Minimize();

        /// <summary>
        /// <c>unsigned int Fl_Window_maximize_state(const Fl_Window* self);</c>
        /// </summary>
        public bool IsMaximized { get; }
    }
}
