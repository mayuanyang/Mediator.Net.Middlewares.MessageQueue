# Mediator.Net.Middlewares.MessageQueue
## A middleware for Mediator.Net to send message to message queue
When you inplementing an application for an organisation that has many different micro services that use message broker like RabbitMQ or Azure service bus to connect from each other, and when a behaviour has done inside your domian, you usually want to boradcast the event to the outside world. This middleware helps you to broadcast the message to your configured message broker.

## Supported Brokers
* RabbitMQ
The exchange will be the full name of your message type
* Azure Service Bus
The topic will be the full  name of your message type

## Settings
* UserNameSetting - User name for RabbitMQ
* PasswordSetting - Password for RabbitMQ
* MessageBrokerUriSetting - The Uri for the message broker
* MessageBrokerSetting - Either RabbitMQ or AzureServiceBus
* AzureTokenProviderKeyNameSetting - The key name fro azure service bus
* AzureTokenProviderSharedAccessKeySetting - The key for azure service bus

## Sample Usage
```C#
	// For RabbitMQ
	var mediatorBuilder = new MediatorBuilder();
    var mediator mediatorBuilder.RegisterHandlers(typeof(Program).Assembly)
        .ConfigurePublishPipe(x =>
        {
            x.UseMessageQueue(() => new BusConfiguration{
				UserNameSetting = ConfigurationManager.AppSettings["UserNameSetting"],
				PasswordSetting = ConfigurationManager.AppSettings["PasswordSetting"],
				MessageBrokerUriSetting = ConfigurationManager.AppSettings["MessageBrokerUriSetting"]
			},
            () => true);
        }).Build();
	
	// TotalCalculatedEvent will be raised by the CalculateTotalCommandHandler
	await mediator.SendAsync(new CalculateTotalCommand(1, 2));
	
	// TotalCalculatedEvent will then be sent to your configured message broker
```

## Mediator.Net
This middleware is to be plugged into Mediator.Net's pipeline, for more information please browse to [Mediator.Net](https://github.com/mayuanyang/Mediator.Net)


## MassTransit
This middleware is using MassTransit as the transport to deliver the message to the broker, for more information please browse to [MassTransit](https://github.com/MassTransit/MassTransit)

