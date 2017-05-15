
using System;
using UnityEventBus.API;

namespace Assets.UnityEventBus.Core
{
    public class SimpleEventArgument : EventArgument
    {
        private string eventName;
        private float timeStamp;
        private object sender;
        private object data;

        public SimpleEventArgument(string eventName, float timeStamp, object sender, object data)
        {
            this.eventName = eventName;
            this.timeStamp = timeStamp;
            this.sender = sender;
            this.data = data;
        }

        #region Impl of EventArgument

        public string EventName { get { return eventName; } }
        public float Timestamp { get { return timeStamp; } }
        public object Sender { get { return sender; } }
        public object Data { get { return data; } }

        #endregion

    }
}
