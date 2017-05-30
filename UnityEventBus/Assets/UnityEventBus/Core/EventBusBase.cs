using Assets.UnityEventBus.Core;
using UnityEngine;
using UnityEventBus.API;

namespace UnityEventBus.Core
{
    public abstract class EventBusBase : EventBus
    {
        protected DelegateRegister register = new DelegateRegister();

        #region Impl of EventBus

        public string Name { get; private set; }

        public void Register(object listener)
        {
            register.Register(listener);
        }

        public void RegisterForEvent(object listener, string eventName)
        {
            register.RegisterForEvent(listener, eventName);
        }

        public void Unregister(object listener)
        {
            register.Unregister(listener);
        }

        public void UnregisterForEvent(object listener, string eventName)
        {
            register.UnregisterForEvent(listener, eventName);
        }

        public bool IsRegistered(object listener)
        {
            return register.IsRegistered(listener);
        }

        public bool IsRegisteredForEvent(object listener, string eventName)
        {
            return register.IsRegisteredForEvent(listener, eventName);
        }

        #endregion

        #region Methods which must be overriden by the derived classes

        public abstract void Post(string eventName);
        public abstract void Post(string eventName, string filter);
        public abstract void Post(string eventName, float delay);
        public abstract void Post(string eventName, string filter, float delay);

        #endregion

        #region Constructor

        protected PostDelayer postDelayer;

        protected EventBusBase(string name)
        {
            Name = name;
            GameObject go = new GameObject(name, typeof(PostDelayer));
            postDelayer = go.GetComponent<PostDelayer>();
            postDelayer.EventBus = this;
        }

        #endregion
    }
}

