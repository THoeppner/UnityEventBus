
using System;

namespace UnityEventBus.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SubscribeAttribute : Attribute
    {
        public string EventName { get; private set; }

        public SubscribeAttribute(string eventName)
        {
            EventName = eventName;
        }
    }

}