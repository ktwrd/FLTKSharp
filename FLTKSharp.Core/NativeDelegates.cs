namespace FLTKSharp.Core;

/// <summary>
/// <c>
/// int (*custom_handler_callback)(Fl_Widget *, int, void *)
/// </c>
/// </summary>
public delegate int FltkWidgetHandleCallback(IntPtr widget, int eventKind, IntPtr data);

public delegate void FltkObjectHandleMethod(IntPtr widget, IntPtr callback, IntPtr data);

/// <summary>
/// Signature of <see cref="CFltkNative.Fl_add_handler"/> functions passed as parameters. 
/// </summary>
public delegate int FltkGlobalEventHandler(int eventId);

/// <summary>
/// <c>
/// void (*custom_draw_callback)(Fl_Widget *, void *)
/// </c>
/// </summary>
public delegate void FltkWidgetCallback(IntPtr widget, IntPtr data);

/// <summary>
/// <c>
/// void (*cb)(Fl_Widget *, int x, int y, int w, int h, void *)
/// </c>
/// </summary>
public delegate void FltkWidgetResizeCallback(IntPtr widget, int x, int y, int width, int height, IntPtr data);

/// <summary>
/// Signature of add_handler functions passed as parameters
/// <code lang="c">
/// typedef int (*Fl_Event_Handler)(int event);
/// </code>
/// </summary>
public delegate int FltkEventHandlerDelegate(int @event);

/// <summary>
/// <c>
/// typedef void (*Fl_Timeout_Handler)(void *data);
/// </c>
/// </summary>
public delegate void FltkTimeoutHandler(IntPtr data);

/// <summary>
/// <c>
/// int (*cb)(int event, void *)
/// </c>
/// </summary>
public delegate int FltkEventDispatchHandler(int @event, IntPtr window);

/// <summary>
/// <c>
/// void (*cb)(int, int, int, int, unsigned int)
/// </c>
/// </summary>
public delegate void FltkBoxDrawHandler(int x, int y, int width, int height, uint color);

/// <summary>
/// <c>
/// int (*)(void *, void *)
/// </c>
/// </summary>
public delegate int FltkSystemHandler(IntPtr @event, IntPtr data);

/// <summary>
/// <c>
/// void (*cb)(int source, void *data)
/// </c>
/// </summary>
public delegate void FltkClipboardNotifyHandler(int source, IntPtr data);

/// <summary>
/// <c>
/// void (*cb)(const char *)
/// </c>
/// </summary>
public delegate void FltkOpenCallbackHandler(IntPtr stringPointer);

/// <summary>
/// <c>
/// void *
/// </c>
/// </summary>
public delegate void FltkAwakeHandler();

public delegate uint FltkContrastFunction(uint colorForeground, uint colorBackground, int context, int size);