
using UnityEventBus.API;
using UnityEventBus.Attributes;

namespace Testing.UnityEventBus
{

    public class FilterTestClass1
    {
        public int StartedCounter { get; set; }

        [Subscribe("Started", Filter="OnlyThat")]
        public void OnStarted(EventArgument e)
        {
            StartedCounter++;
        }
    }
}