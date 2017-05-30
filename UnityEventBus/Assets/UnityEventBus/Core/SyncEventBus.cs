using Assets.UnityEventBus.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEventBus.API;

namespace UnityEventBus.Core
{
    public class SyncEventBus : EventBusBase
    {
        #region Overriden methods

        public override void Post(string eventName)
        {
            Post(eventName, "", 0);
        }

        public override void Post(string eventName, string filter)
        {
            Post(eventName, filter, 0);
        }

        public override void Post(string eventName, float delay)
        {
            Post(eventName, "", delay);
        }

        public override void Post(string eventName, string filter, float delay)
        {
            if (string.IsNullOrEmpty(eventName)) return;

            if (delay > 0)
                postDelayer.Post(eventName, filter, delay);
            else
            {
                EventArgument ea = new SimpleEventArgument(eventName, Time.time, null, null);
                List<DelegateDefinition> delegatesToFire = register.GetDelegatesForEvent(eventName, filter);

                foreach (DelegateDefinition dd in delegatesToFire)
                    dd.delegateToFire.DynamicInvoke(ea);
            }
        }

        #endregion

        #region Constructor

        public SyncEventBus(string name)
            : base(name)
        {
        }

        #endregion
    }

}

