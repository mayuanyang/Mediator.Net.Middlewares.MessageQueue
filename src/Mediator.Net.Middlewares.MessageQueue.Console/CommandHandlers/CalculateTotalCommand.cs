using System.Threading.Tasks;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Mediator.Net.Middlewares.MessageQueue.Console.Commands;
using Mediator.Net.Middlewares.MessageQueue.Console.Events;

namespace Mediator.Net.Middlewares.MessageQueue.Console.CommandHandlers
{
    class CalculateTotalCommandHandler : ICommandHandler<CalculateTotalCommand>
    {
        public async Task Handle(ReceiveContext<CalculateTotalCommand> context)
        {
            await context.PublishAsync(new TotalCalculatedEvent(context.Message.Arg1 + context.Message.Arg2));
        }
    }
}
