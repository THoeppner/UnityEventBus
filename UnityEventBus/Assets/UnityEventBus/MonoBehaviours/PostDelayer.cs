using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventBus.API;

namespace UnityEventBus.Core
{
    public class PostDelayer : MonoBehaviour
    {
        private struct DelayArguement
        {
            public string EventName;
            public string Filter;
            public float Delay;
        }

        public EventBus EventBus { get; set; }

        public void Post(string eventName, string filter, float delay)
        {
            StartCoroutine("PostDelayed", new DelayArguement() { Delay = delay, EventName = eventName, Filter = filter });
        }

        private IEnumerator PostDelayed(DelayArguement arg)
        {
            yield return new WaitForSeconds(arg.Delay);
            EventBus.Post(arg.EventName, arg.Filter);
        }
    }
}
