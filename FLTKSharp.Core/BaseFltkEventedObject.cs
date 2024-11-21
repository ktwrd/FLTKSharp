using FLTKSharp.Core.Events;
using NLog;
using System.Runtime.InteropServices;

namespace FLTKSharp.Core
{
    public class BaseFltkEventedObject : BaseFltkObject
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        internal BaseFltkEventedObject(IntPtr ptr)
            : base(ptr)
        {
            _log.Properties["Pointer"] = ptr;
            _fltkEventHandlerPtr = Marshal.GetFunctionPointerForDelegate<FltkWidgetHandleCallback>(FltkEventHandler);
        }
        private IntPtr _fltkEventHandlerPtr;

        protected override void Disposing(bool disposed)
        {
            base.Disposing(disposed);
        }

        private void InitializeEventHandling()
        {
            FlObjectHandle(Pointer, _fltkEventHandlerPtr, IntPtr.Zero);
        }
        private int FltkEventHandler(IntPtr widget, int eventKind, IntPtr data)
        {
            OnUnsafeEvent(new(widget, eventKind, data));
            return 0;
        }
        private FltkObjectHandleMethod _flObjectHandle = (_, _, _) => { };
        protected virtual FltkObjectHandleMethod FlObjectHandle
        {
            get => _flObjectHandle;
            set
            {
                _flObjectHandle = value;
                InitializeEventHandling();
            }
        }

        private readonly List<EventHandler<FltkUnsafeEventArgs>> _unsafeEventHandlers = [];
        internal event EventHandler<FltkUnsafeEventArgs> UnsafeEvent
        {
            add
            {
                lock (_unsafeEventHandlers)
                {
                    _unsafeEventHandlers.Add(value);
                }
            }
            remove
            {
                lock (_unsafeEventHandlers)
                {
                    _unsafeEventHandlers.Remove(value);
                }
            }
        }
        private void OnUnsafeEvent(FltkUnsafeEventArgs ea)
        {
            lock (_unsafeEventHandlers)
            {
                foreach (var item in _unsafeEventHandlers)
                {
                    try
                    {
                        item?.Invoke(this, ea);
                    }
                    catch (Exception ex)
                    {
                        _log.Trace(ex.ToString());
                    }
                }
            }
        }
    }
}
