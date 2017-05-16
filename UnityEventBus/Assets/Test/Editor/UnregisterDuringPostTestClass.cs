
using UnityEventBus.API;
using UnityEventBus.Attributes;

namespace Testing.UnityEventBus
{

    public class UnregisterDuringPostTestClass
    {
        public EventBus EventBus { get; set; }

        [Subscribe("UnregisterEvent")]
        public void OnUnregisterEvent(EventArgument e)
        {
            EventBus.Unregister(this);
        }
    }
}