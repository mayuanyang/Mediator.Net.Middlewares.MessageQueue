using System;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using Microsoft.ServiceBus;
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
                        if (string.IsNullOrEmpty(busConfiguration.MessageBrokerUri))
                        {
                            throw new MissingFieldException("MessageBrokerUri is missing");
                        }

                        if (busConfiguration.MessageBroker == MessageBroker.RabbitMQ)
                        {
                            _busToBeUsed = Bus.Factory.CreateUsingRabbitMq(cfg =>
                            {
                                cfg.Host(new Uri(busConfiguration.MessageBrokerUri), x =>
                                {
                                    x.Username(busConfiguration.UserName);
                                    x.Password(busConfiguration.Password);
                                });

                            });
                        }
                        else if (busConfiguration.MessageBroker == MessageBroker.AzureServiceBus)
                        {
                            if (string.IsNullOrEmpty(busConfiguration.AzureTokenProviderKeyName))
                            {
                                throw new MissingFieldException("AzureTokenProviderKeyName is missing");
                            }

                            if (string.IsNullOrEmpty(busConfiguration.AzureTokenProviderSharedAccessKey))
                            {
                                throw new MissingFieldException("AzureTokenProviderSharedAccessKey is missing");
                            }

                            _busToBeUsed = Bus.Factory.CreateUsingAzureServiceBus(cfg =>
                            {
                                var host = cfg.Host(new Uri(busConfiguration.MessageBrokerUri), h =>
                                {
                                    h.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(busConfiguration.AzureTokenProviderKeyName,
                                        busConfiguration.AzureTokenProviderSharedAccessKey);
                                });
                            });
                        }
                        else
                        {
                            throw new NotSupportedException("Not supported MessageBroker");
                        }
                        
                    }
                }
            }
            configurator.AddPipeSpecification(new MessageQueueSpecification(shouldExecute, _busToBeUsed));
        }
    }
}
