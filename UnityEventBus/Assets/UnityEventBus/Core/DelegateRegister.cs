using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEventBus.Attributes;
using System;
using UnityEventBus.API;

namespace Assets.UnityEventBus.Core
{
    public class DelegateRegister
    {
        private delegate void EventArgumentDelegate(EventArgument e);

        List<DelegateDefinition> delegateDefinitions = new List<DelegateDefinition>();

        public void Register(object listener)
        {
            if (listener == null) return; 

            Dictionary<string, MethodInfo> methodInfos = GetMethodInfosWithValidSubscribeAttribute(listener);
            foreach (KeyValuePair<string, MethodInfo> item in methodInfos)
            {
                if (IsRegisteredForEvent(listener, item.Key)) continue; // Already registered

                DelegateDefinition di = new DelegateDefinition() { eventName = item.Key, target = listener };
                di.delegateToFire = Delegate.CreateDelegate(typeof(EventArgumentDelegate), listener, item.Value);
                delegateDefinitions.Add(di);
            }
        }

        public void RegisterForEvent(object listener, string eventName)
        {
            if ((listener == null) || (string.IsNullOrEmpty(eventName))) return; // Invalid arguments
            if (IsRegisteredForEvent(listener, eventName)) return; // already registered

            MethodInfo mi = GetMethodInfoForEventName(listener, eventName);
            if (mi == null) return;

            DelegateDefinition dd = new DelegateDefinition() { eventName = eventName, target = listener };
            dd.delegateToFire = Delegate.CreateDelegate(typeof(EventArgumentDelegate), listener, mi);
            delegateDefinitions.Add(dd);
        }

        public void Unregister(object listener)
        {
            List<DelegateDefinition> delegatesToRemove = delegateDefinitions.FindAll(dd => dd.target == listener);
            foreach (DelegateDefinition di in delegatesToRemove)
                delegateDefinitions.Remove(di);
        }

        public void UnregisterForEvent(object listener, string eventName)
        {
            List<DelegateDefinition> delegatesToRemove = delegateDefinitions.FindAll(dd => dd.target == listener && dd.eventName == eventName);
            foreach (DelegateDefinition di in delegatesToRemove)
                delegateDefinitions.Remove(di);
        }

        public bool IsRegistered(object listener)
        {
            if (listener == null) return false;
            List<DelegateDefinition> delegates = delegateDefinitions.FindAll(dd => dd.target == listener);
            return delegates.Count > 0;
        }

        public bool IsRegisteredForEvent(object listener, string eventName)
        {
            if ((listener == null) || (string.IsNullOrEmpty(eventName))) return false;
            List<DelegateDefinition> delegates = delegateDefinitions.FindAll(dd => dd.target == listener && dd.eventName == eventName);
            return delegates.Count > 0;
        }

        public List<DelegateDefinition> GetDelegatesForEvent(string eventName)
        {
            return delegateDefinitions.FindAll(dd => dd.eventName == eventName);
        }

        private Dictionary<string, MethodInfo> GetMethodInfosWithValidSubscribeAttribute(object listener)
        {
            Dictionary<string, MethodInfo> infos = new Dictionary<string, MethodInfo>();

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

                infos[attrs[0].EventName] = mi;
            }
            return infos;
        }

        private MethodInfo GetMethodInfoForEventName(object listener, string eventName)
        {
            Dictionary<string, MethodInfo> infos = GetMethodInfosWithValidSubscribeAttribute(listener);
            MethodInfo mi;
            infos.TryGetValue(eventName, out mi);
            return mi;
        }
    }
}
