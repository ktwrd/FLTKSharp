using static FLTKSharp.Core.CFltkNative;

namespace FLTKSharp.Core.Events
{
    public class FltkMouseEventArgs : EventArgs
    {
        /// <summary>
        /// X Position of the mouse
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Y Position of the mouse
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// What mouse buttons were being pressed?
        /// </summary>
        public FltkButtonEventState Button { get; private set; }
        /// <summary>
        /// What is the current event state?
        /// </summary>
        public FltkEventState EventState { get; private set; }
        internal FltkMouseEventArgs()
        {
            X = Fl_event_x();
            Y = Fl_event_y();
            var eventState = Fl_event_state();
            EventState = (FltkEventState)eventState;
            if ((eventState & (int)FltkButtonEventState.Any) == eventState)
            {
                Button = (FltkButtonEventState)eventState;
            }
            else
            {
                Button = FltkButtonEventState.None;
            }
        }
    }
}
