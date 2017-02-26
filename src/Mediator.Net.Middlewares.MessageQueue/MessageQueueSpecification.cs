using System;
using System.Threading.Tasks;
using MassTransit;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Mediator.Net.Pipeline;

namespace Mediator.Net.Middlewares.MessageQueue
{
    public class MessageQueueSpecification : IPipeSpecification<IPublishContext<IEvent>>
    {
        private readonly Func<bool> _shouldExecute;
        private readonly IBusControl _bus;

        public MessageQueueSpecification(Func<bool> shouldExecute, IBusControl bus )
        {
            _shouldExecute = shouldExecute;
            _bus = bus;
        }
        public bool ShouldExecute(IPublishContext<IEvent> context)
        {
            return _shouldExecute == null || _shouldExecute();
        }

        public async Task ExecuteBeforeConnect(IPublishContext<IEvent> context)
        {
            await Task.FromResult(0);
        }

        public async Task ExecuteAfterConnect(IPublishContext<IEvent> context)
        {
            await _bus.Publish(context.Message);
        }

        public void OnException(Exception ex, IPublishContext<IEvent> context)
        {
            throw ex;
        }
    }
}