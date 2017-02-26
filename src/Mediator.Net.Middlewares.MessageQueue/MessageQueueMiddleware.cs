using System;
using MassTransit;
using MNPublishPipeConfigurator = Mediator.Net.Pipeline.IPublishPipeConfigurator;

namespace Mediator.Net.Middlewares.MessageQueue
{
    public static class MessageQueueMiddleware
    {
        private static IBus _busToBeUsed;

        public static void UseMessageQueue(this MNPublishPipeConfigurator configurator,
            IBusConfiguration busConfiguration, Func<bool> shouldExecute, IBus bus = null)
        {
            _busToBeUsed = bus;
            if (bus == null)
            {
                bus = configurator.DependancyScope.Resolve<IBusControl>();
                if (bus == null)
                {
                    if (_busToBeUsed == null)
                    {
                        _busToBeUsed = Bus.Factory.CreateUsingRabbitMq(cfg =>
                        {
                            var uri = busConfiguration.MessageBrokerUri;
                            cfg.Host(new Uri(uri), x =>
                            {
                                x.Username(busConfiguration.UserName);
                                x.Password(busConfiguration.Password);
                            });

                        });
                    }
                }
            }
            configurator.AddPipeSpecification(new MessageQueueSpecification(shouldExecute, _busToBeUsed));
        }
    }
}
