
using System;

namespace UnityEventBus.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SubscribeAttribute : Attribute
    {
        /// <summary>
        /// The name of the event to subscribe
        /// </summary>
        public string EventName { get; private set; }

        /// <summary>
        /// A filter
        /// </summary>
        public string Filter { get; set; }

        public SubscribeAttribute(string eventName)
        {
            EventName = eventName;
            Filter = "";
        }
    }

}