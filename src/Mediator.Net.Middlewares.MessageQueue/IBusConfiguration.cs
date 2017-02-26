namespace Mediator.Net.Middlewares.MessageQueue
{
    public interface IBusConfiguration
    {
        string UserName { get; }
        string Password { get; }
        string MessageBrokerUri { get; }
        MessageBroker MessageBroker { get; }
        string AzureTokenProviderKeyName { get; }
        string AzureTokenProviderSharedAccessKey { get; }
    }
}
