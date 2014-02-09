using System;
using System.Collections.Generic;
using System.Text;

namespace DMS.MapLibrary
{
    /// <summary>
    /// Common class to propagate the error messages for recording ang logging purposes.
    /// </summary>
    public static class EventProvider
    {
        /// <summary>
        /// Enum for The user editing modes (panning or zooming).
        /// </summary>
        public enum EventTypes
        {
            /// <summary>
            /// Error type
            /// </summary>
            Error,
            /// <summary>
            /// Warning type
            /// </summary>
            Warning,
            /// <summary>
            /// Information type
            /// </summary>
            Information
        }
        /// <summary>
        /// The signature of the EventMessage event handler. Raised when an event is happening within the application.
        /// </summary>
        public delegate void EventMessageEventHandler(object sender, string Message, EventTypes type);
        
        /// <summary>
        /// The EventMessage event handler. Raised when an event is happening within the application.
        /// </summary>
        public static event EventMessageEventHandler EventMessage;

        public static void RaiseEventMessage(object sender, string Message, EventTypes type)
        {
            if (EventMessage != null)
            {
                EventMessage(sender, Message, type);
            }
        }
    }
}
