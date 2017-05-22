using System;
using System.Collections.Generic;
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

            Dictionary<string, ObjectInspectionHelper> methodInfos = ObjectInspector.GetMethodInfosWithValidSubscribeAttribute(listener);
            foreach (KeyValuePair<string, ObjectInspectionHelper> item in methodInfos)
            {
                if (IsRegisteredForEvent(listener, item.Key)) continue; // Already registered
                delegateDefinitions.Add(CreateDelegateDefinition(listener, item.Value));
            }
        }

        public void RegisterForEvent(object listener, string eventName)
        {
            if ((listener == null) || (string.IsNullOrEmpty(eventName))) return; // Invalid arguments
            if (IsRegisteredForEvent(listener, eventName)) return; // already registered

            ObjectInspectionHelper helper = ObjectInspector.GetMethodInfoForSubsribedEventEvent(listener, eventName);
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

        public List<DelegateDefinition> GetDelegatesForEvent(string eventName)
        {
            return delegateDefinitions.FindAll(dd => dd.eventName == eventName);
        }

        public List<DelegateDefinition> GetDelegatesForEvent(string eventName, string filter)
        {
            return delegateDefinitions.FindAll(dd => dd.eventName == eventName && dd.filter == filter);
        }

        #region private helper

        private DelegateDefinition CreateDelegateDefinition(object listener, ObjectInspectionHelper helper)
        {
            DelegateDefinition dd = new DelegateDefinition() { eventName = helper.subscribeAttribute.EventName, target = listener, filter = helper.subscribeAttribute.Filter };
            dd.delegateToFire = Delegate.CreateDelegate(typeof(EventArgumentDelegate), listener, helper.methodInfo);
            return dd;
        }

        #endregion
    }
}
