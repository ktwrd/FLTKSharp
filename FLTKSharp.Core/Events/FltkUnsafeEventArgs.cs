using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLTKSharp.Core.Events
{
    internal class FltkUnsafeEventArgs : EventArgs
    {
        internal IntPtr Widget { get; private set; }
        internal int EventValue { get; private set; }
        public FltkEvent Event { get; private set; }
        public IntPtr DataPointer { get; private set; }

        internal FltkUnsafeEventArgs(IntPtr widget, int @event, IntPtr data)
        {
            Widget = widget;
            EventValue = @event;
            Event = InternalHelper.ParseEnum(@event, FltkEvent.Unknown);
            DataPointer = data;
        }
    }
}
