
namespace UnityEventBus.API
{
    public interface EventBus
    {
        string Name { get; set; }

        void Register(object listener);

        void Unregister(object listener, string eventName);

        bool IsRegistered(object listener);

        void Post(string eventName);
    }
}
