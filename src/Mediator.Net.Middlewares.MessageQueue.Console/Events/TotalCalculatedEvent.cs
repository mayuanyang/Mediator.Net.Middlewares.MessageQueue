using Mediator.Net.Contracts;

namespace Mediator.Net.Middlewares.MessageQueue.Console.Events
{
    public class TotalCalculatedEvent : IEvent
    {
        public decimal Total { get; }

        public TotalCalculatedEvent(decimal total)
        {
            Total = total;
        }
    }
}
