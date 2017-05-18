
using UnityEventBus.API;
using UnityEventBus.Attributes;

namespace Testing.UnityEventBus
{

    public class FilterTestClass
    {
        public int StartedCounter { get; set; }

        [Subscribe("Started", Filter="OnlyThis")]
        public void OnStarted(EventArgument e)
        {
            StartedCounter++;
        }

    }
}