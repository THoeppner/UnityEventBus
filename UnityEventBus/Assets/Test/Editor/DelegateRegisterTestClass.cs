
using UnityEventBus.API;
using UnityEventBus.Attributes;

namespace Testing.UnityEventBus
{
    public class DelegateRegisterTestClass
    {
        [Subscribe("Started")]
        public void OnStarted(EventArgument e)
        {
        }

        [Subscribe("Ended")]
        public void OnEnded(EventArgument e)
        {
        }

        [Subscribe("GetHit")]
        public void OnGetHit(EventArgument e)
        {
        }

        public void OnNoAttribute(EventArgument e)
        {
        }
    }

}
