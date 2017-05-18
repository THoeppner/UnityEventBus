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
        private class DelegateRegisterHelper
        {
            public SubscribeAttribute subscribeAttribute;
            public MethodInfo methodInfo;
        }

        private delegate void EventArgumentDelegate(EventArgument e);

        List<DelegateDefinition> delegateDefinitions = new List<DelegateDefinition>();

        public void Register(object listener)
        {
            if (listener == null) return; 

            Dictionary<string, DelegateRegisterHelper> methodInfos = GetMethodInfosWithValidSubscribeAttribute(listener);
            foreach (KeyValuePair<string, DelegateRegisterHelper> item in methodInfos)
            {
                if (IsRegisteredForEvent(listener, item.Key)) continue; // Already registered
                delegateDefinitions.Add(CreateDelegateDefinition(listener, item.Value));
            }
        }

        public void RegisterForEvent(object listener, string eventName)
        {
            if ((listener == null) || (string.IsNullOrEmpty(eventName))) return; // Invalid arguments
            if (IsRegisteredForEvent(listener, eventName)) return; // already registered

            DelegateRegisterHelper helper = GetDelegateRegisterHelperForEvent(listener, eventName);
            if (helper != null) 
                delegateDefinitions.Add(CreateDelegateDefinition(listener, helper));
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

        // TODO: Create class ObjectInspector to find the subscribe attributes

        public List<DelegateDefinition> GetDelegatesForEvent(string eventName)
        {
            return delegateDefinitions.FindAll(dd => dd.eventName == eventName);
        }

        public List<DelegateDefinition> GetDelegatesForEvent(string eventName, string filter)
        {
            return delegateDefinitions.FindAll(dd => dd.eventName == eventName && dd.filter == filter);
        }


        private DelegateRegisterHelper GetDelegateRegisterHelperForEvent(object listener, string eventName)
        {
            DelegateRegisterHelper helper;
            Dictionary<string, DelegateRegisterHelper> infos = GetMethodInfosWithValidSubscribeAttribute(listener);
            infos.TryGetValue(eventName, out helper);
            return helper;
        }

        private Dictionary<string, DelegateRegisterHelper> GetMethodInfosWithValidSubscribeAttribute(object listener)
        {
            Dictionary<string, DelegateRegisterHelper> infos = new Dictionary<string, DelegateRegisterHelper>();

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
                infos[attrs[0].EventName] = new DelegateRegisterHelper() { subscribeAttribute = attrs[0], methodInfo = mi };
            }
            return infos;
        }

        private DelegateDefinition CreateDelegateDefinition(object listener, DelegateRegisterHelper helper)
        {
            DelegateDefinition dd = new DelegateDefinition() { eventName = helper.subscribeAttribute.EventName, target = listener, filter = helper.subscribeAttribute.Filter };
            dd.delegateToFire = Delegate.CreateDelegate(typeof(EventArgumentDelegate), listener, helper.methodInfo);
            return dd;
        }
    }
}
