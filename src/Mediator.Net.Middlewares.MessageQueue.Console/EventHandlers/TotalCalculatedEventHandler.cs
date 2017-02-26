using System.Threading.Tasks;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Mediator.Net.Middlewares.MessageQueue.Console.Events;

namespace Mediator.Net.Middlewares.MessageQueue.Console.EventHandlers
{
    class TotalCalculatedEventHandler : IEventHandler<TotalCalculatedEvent>
    {
        public Task Handle(IReceiveContext<TotalCalculatedEvent> context)
        {
            return Task.FromResult(0);
        }
    }
}
