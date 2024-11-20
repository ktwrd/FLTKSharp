namespace FLTKSharp.Core;

/// <summary>
/// <c>
/// int (*custom_handler_callback)(Fl_Widget *, int, void *)
/// </c>
/// </summary>
public delegate int FltkWidgetHandleCallback(IntPtr widget, int eventKind, IntPtr data);

public delegate void FltkObjectHandleMethod(IntPtr widget, FltkWidgetHandleCallback callback, IntPtr data);

/// <summary>
/// Signature of <see cref="CFltkNative.Fl_add_handler"/> functions passed as parameters. 
/// </summary>
internal delegate int FltkGlobalEventHandler(int eventId);

/// <summary>
/// <c>
/// void (*custom_draw_callback)(Fl_Widget *, void *)
/// </c>
/// </summary>
internal delegate void FltkWidgetCallback(IntPtr widget, IntPtr data);

/// <summary>
/// <c>
/// void (*cb)(Fl_Widget *, int x, int y, int w, int h, void *)
/// </c>
/// </summary>
internal delegate void FltkWidgetResizeCallback(IntPtr widget, int x, int y, int width, int height, IntPtr data);

/// <summary>
/// Signature of add_handler functions passed as parameters
/// <code lang="c">
/// typedef int (*Fl_Event_Handler)(int event);
/// </code>
/// </summary>
internal delegate int FltkEventHandlerDelegate(int @event);

/// <summary>
/// <c>
/// typedef void (*Fl_Timeout_Handler)(void *data);
/// </c>
/// </summary>
internal delegate void FltkTimeoutHandler(IntPtr data);

/// <summary>
/// <c>
/// int (*cb)(int event, void *)
/// </c>
/// </summary>
internal delegate int FltkEventDispatchHandler(int @event, IntPtr window);

/// <summary>
/// <c>
/// void (*cb)(int, int, int, int, unsigned int)
/// </c>
/// </summary>
internal delegate void FltkBoxDrawHandler(int x, int y, int width, int height, uint color);

/// <summary>
/// <c>
/// int (*)(void *, void *)
/// </c>
/// </summary>
internal delegate int FltkSystemHandler(IntPtr @event, IntPtr data);

/// <summary>
/// <c>
/// void (*cb)(int source, void *data)
/// </c>
/// </summary>
internal delegate void FltkClipboardNotifyHandler(int source, IntPtr data);

/// <summary>
/// <c>
/// void (*cb)(const char *)
/// </c>
/// </summary>
internal delegate void FltkOpenCallbackHandler(IntPtr stringPointer);

/// <summary>
/// <c>
/// void *
/// </c>
/// </summary>
internal delegate void FltkAwakeHandler();

internal delegate uint FltkContrastFunction(uint colorForeground, uint colorBackground, int context, int size);