using UnityEventBus.Core;

namespace Testing.UnityEventBus
{
    public class SubscribeClass
    {
        [Subscribe("Started")]
        public void OnStarted(string msg)
        {
        }
    }

}
