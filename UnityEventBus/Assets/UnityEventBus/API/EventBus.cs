
namespace UnityEventBus.API
{
    public interface EventBus
    {
        /// <summary>
        /// The name of this event bus
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Registers all methods with the Subscribe attribute of the given object 
        /// </summary>
        /// <param name="listener"></param>
        void Register(object listener);

        /// <summary>
        /// Registers only the method of the given event of the given object
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="eventName"></param>
        void RegisterForEvent(object listener, string eventName);

        /// <summary>
        /// Unregister all methods of the given object
        /// </summary>
        /// <param name="listener"></param>
        void Unregister(object listener);

        /// <summary>
        /// Unregisters the method for the given event name of the given object
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="eventName"></param>
        void UnregisterForEvent(object listener, string eventName);

        /// <summary>
        /// Checks, if the given object is registered
        /// </summary>
        /// <param name="listener"></param>
        /// <returns></returns>
        bool IsRegistered(object listener);

        /// <summary>
        /// Checks, if the given object is registered for the given event
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        bool IsRegisteredForEvent(object listener, string eventName);

        void Post(string eventName);
    }
}
