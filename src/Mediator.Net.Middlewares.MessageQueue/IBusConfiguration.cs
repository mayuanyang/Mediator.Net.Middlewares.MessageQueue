namespace Mediator.Net.Middlewares.MessageQueue
{
    public interface IBusConfiguration
    {
        string UserName { get; }
        string Password { get; }
        string MessageBrokerUri { get; }
    }
}
