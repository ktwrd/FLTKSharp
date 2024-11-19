using FLTKSharp.Core.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLTKSharp.Core
{
    public class BaseFltkEventedObject : BaseFltkObject
    {
        internal BaseFltkEventedObject(IntPtr ptr)
            : base(ptr)
        {
            FlObjectHandle = (_, _, _) => { };
            InitializeEventHandling();
        }

        protected override void Disposing(bool disposed)
        {
            base.Disposing(disposed);
        }

        private void InitializeEventHandling()
        {
            FlObjectHandle?.Invoke(Pointer, FltkEventHandler, IntPtr.Zero);
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
                if (value != _flObjectHandle)
                {
                    _flObjectHandle = value;
                    InitializeEventHandling();
                }
            }
        }

        private List<EventHandler<FltkUnsafeEventArgs>> _unsafeEventHandlers = [];
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
            foreach (var item in _unsafeEventHandlers)
            {
                try
                {
                    item?.Invoke(this, ea);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                }
            }
        }
    }
}
