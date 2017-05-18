using Assets.UnityEventBus.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEventBus.API;

namespace UnityEventBus.Core
{
    public class SyncEventBus : EventBusBase 
    {
        public override void Post(string eventName)
        {
            if (string.IsNullOrEmpty(eventName)) return;

            EventArgument ea = new SimpleEventArgument(eventName, Time.time, null, null);

            List<DelegateDefinition> delegatesToFire = register.GetDelegatesForEvent(eventName);
            foreach(DelegateDefinition dd in delegatesToFire)
                dd.delegateToFire.DynamicInvoke(ea);
        }

        public override void Post(string eventName, string filter)
        {
            if (string.IsNullOrEmpty(eventName)) return;

            EventArgument ea = new SimpleEventArgument(eventName, Time.time, null, null);

            List<DelegateDefinition> delegatesToFire = register.GetDelegatesForEvent(eventName, filter);
            foreach (DelegateDefinition dd in delegatesToFire)
                dd.delegateToFire.DynamicInvoke(ea);
        }
    }

}

