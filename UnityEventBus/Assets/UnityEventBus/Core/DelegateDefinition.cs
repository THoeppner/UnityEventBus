
using System;

namespace Assets.UnityEventBus.Core
{
    public class DelegateDefinition
    {
        public string eventName;
        public string filter = "";
        public Delegate delegateToFire;
        public object target;
    }
}
