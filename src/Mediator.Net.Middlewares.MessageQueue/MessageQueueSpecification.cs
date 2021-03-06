using System;
using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Mediator.Net.Pipeline;

namespace Mediator.Net.Middlewares.MessageQueue
{
    public class MessageQueueSpecification<TMessage> : IPipeSpecification<IPublishContext<TMessage>>
        where TMessage : class, IEvent
    {
        private readonly Func<bool> _shouldExecute;
        private readonly IBus _bus;

        public MessageQueueSpecification(Func<bool> shouldExecute, IBus bus )
        {
            _shouldExecute = shouldExecute;
            _bus = bus;
        }
        public bool ShouldExecute(IPublishContext<TMessage> context)
        {
            return _shouldExecute == null || _shouldExecute();
        }

        public async Task ExecuteBeforeConnect(IPublishContext<TMessage> context)
        {
            await Task.FromResult(0);
        }

        public async Task ExecuteAfterConnect(IPublishContext<TMessage> context)
        {
            await _bus.Publish(context.Message, context.Message.GetType());
        }

        public void OnException(Exception ex, IPublishContext<TMessage> context)
        {
            throw ex;
        }
    }
}