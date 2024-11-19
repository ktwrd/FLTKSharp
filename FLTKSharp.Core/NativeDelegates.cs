namespace FLTKSharp.Core;

public delegate int FltkWidgetHandleCallback(IntPtr widget, int eventKind, IntPtr data);

public delegate void FltkObjectHandleMethod(IntPtr widget, FltkWidgetHandleCallback callback, IntPtr data);

/// <summary>
/// Signature of <see cref="CFltkNative.Fl_add_handler"/> functions passed as parameters. 
/// </summary>
internal delegate int FltkGlobalEventHandler(int eventId);