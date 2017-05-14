using Assets.UnityEventBus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEventBus.API;

namespace UnityEventBus.Core
{
    public delegate void StringMethod(string s);

    public abstract class EventBusBase : EventBus
    {
        protected List<DelegateDefinition> delegateDefinitions = new List<DelegateDefinition>();

        #region Impl of EventBus

        public string Name { get; set; }

        public bool IsRegistered(object listener)
        {
            if (listener == null) return false;
            List<DelegateDefinition> delegatesToRemove = delegateDefinitions.FindAll(di => di.target == listener);
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

                DelegateDefinition di = new DelegateDefinition() { eventName = attrs[0].EventName, target = listener };
                di.delegateToFire = Delegate.CreateDelegate(typeof(StringMethod), listener, mi);
                delegateDefinitions.Add(di);
            }
        }

        public void Unregister(object listener, string eventName)
        {
            if (string.IsNullOrEmpty(eventName)) return;

            List<DelegateDefinition> delegatesToRemove = delegateDefinitions.FindAll(di => di.eventName == eventName);
            foreach (DelegateDefinition di in delegatesToRemove)
                delegateDefinitions.Remove(di);
        }

        #endregion

        public abstract void Post(string eventName);
    }
}

