using Assets.UnityEventBus.Core;
using System.Collections.Generic;

namespace UnityEventBus.Core
{
    public class SyncEventBus : EventBusBase 
    {
        public override void Post(string eventName)
        {
            if (string.IsNullOrEmpty(eventName)) return;

            List<DelegateDefinition> delegatesToFire = delegateDefinitions.FindAll(di => di.eventName == eventName);
            foreach(DelegateDefinition di in delegatesToFire)
                di.delegateToFire.DynamicInvoke(eventName);
        }
    }

}

