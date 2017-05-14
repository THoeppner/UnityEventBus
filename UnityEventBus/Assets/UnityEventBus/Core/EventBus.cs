using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityEventBus.Core
{
    public delegate void StringMethod(string s);

    public class EventBus 
    {
        private class DelegateInformation
        {
            public string eventName;
            public Delegate delegateToFire;
            public object target;
        }

        List<DelegateInformation> delegates = new List<DelegateInformation>();

        public bool IsRegistered(object listener)
        {
            if (listener == null) return false;
            List<DelegateInformation> delegatesToRemove = delegates.FindAll(di => di.target == listener);
            return delegatesToRemove.Count > 0;
        }


        public void Register(object listener)
        {
            // Find all methods which have the attribute Subscribe
            MethodInfo[] methods = listener.GetType().GetMethods()
                                    .Where(m => m.GetCustomAttributes(typeof(SubscribeAttribute), false).Length > 0)
                                    .ToArray();

            foreach (MethodInfo mi in methods)
            {
                // We only support one SubscribeAttribute per method
                SubscribeAttribute[] attrs = (SubscribeAttribute[])mi.GetCustomAttributes(typeof(SubscribeAttribute), false);
                if (string.IsNullOrEmpty(attrs[0].EventName))
                    continue;

                DelegateInformation di = new DelegateInformation() { eventName = attrs[0].EventName, target = listener };
                di.delegateToFire = Delegate.CreateDelegate(typeof(StringMethod), listener, mi);
                delegates.Add(di);
            }
        }


        public void Unregister(object listener, string eventName)
        {
            if (string.IsNullOrEmpty(eventName)) return;

            List<DelegateInformation> delegatesToRemove = delegates.FindAll(di => di.eventName == eventName);
            foreach (DelegateInformation di in delegatesToRemove)
            {
                delegates.Remove(di);
            }
        }

        public void Post(string eventName)
        {
            if (string.IsNullOrEmpty(eventName)) return;

            List<DelegateInformation> delegatesToFire = delegates.FindAll(di => di.eventName == eventName);
            foreach(DelegateInformation di in delegatesToFire)
            {
                di.delegateToFire.DynamicInvoke(eventName);
            }
        }
    }

}

