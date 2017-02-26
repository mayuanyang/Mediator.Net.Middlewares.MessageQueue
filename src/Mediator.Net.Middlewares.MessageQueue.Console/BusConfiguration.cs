using Mediator.Net.Middlewares.MessageQueue.Console.Settings;

namespace Mediator.Net.Middlewares.MessageQueue.Console
{
    class BusConfiguration : IBusConfiguration
    {
        public BusConfiguration(
            UserNameSetting userName, 
            PasswordSetting password, 
            MessageBrokerUriSetting messageBrokerUri, 
            MessageBrokerSetting messageBroker, 
            AzureTokenProviderKeyNameSetting azureTokenProviderKeyName, 
            AzureTokenProviderSharedAccessKeySetting azureTokenProviderSharedAccessKey)
        {
            UserName = userName;
            Password = password;
            MessageBrokerUri = messageBrokerUri;
            MessageBroker = messageBroker;
            AzureTokenProviderKeyName = azureTokenProviderKeyName;
            AzureTokenProviderSharedAccessKey = azureTokenProviderSharedAccessKey;
        }
        public string UserName { get; }
        public string Password { get; }
        public string MessageBrokerUri { get; }
        public MessageBroker MessageBroker { get; }
        public string AzureTokenProviderKeyName { get; }
        public string AzureTokenProviderSharedAccessKey { get; }
    }
}
