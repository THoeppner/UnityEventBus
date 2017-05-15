
namespace UnityEventBus.API
{
    public interface EventArgument
    {
        /// <summary>
        /// The name of the event
        /// </summary>
        string EventName { get; }

        /// <summary>
        /// The time when the event happens
        /// </summary>
        float Timestamp { get; }

        /// <summary>
        /// The sender of the event
        /// </summary>
        object Sender { get; }

        /// <summary>
        /// Additional data
        /// </summary>
        object Data { get; }
    }
}
