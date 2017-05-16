
using UnityEventBus.API;
using UnityEventBus.Attributes;

namespace Testing.UnityEventBus
{

    public class PostTestClass
    {
        public int StartedCounter { get; set; }
        public int UnregisterEventCounter { get; set; }

        [Subscribe("Started")]
        public void OnStarted(EventArgument e)
        {
            StartedCounter++;
        }

        [Subscribe("UnregisterEvent")]
        public void OnUnregisterEvent(EventArgument e)
        {
            UnregisterEventCounter++;
        }
    }
}