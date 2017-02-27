using System.Threading.Tasks;
using Autofac;
using ConfigInjector.Configuration;
using Mediator.Net.Contracts;
using Mediator.Net.Middlewares.MessageQueue.Console.Commands;
using Mediator.Net.Middlewares.MessageQueue.Console.Events;

namespace Mediator.Net.Middlewares.MessageQueue.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
            System.Console.Read();
        }

        private static async Task MainAsync()
        {
            var containerBuilder = new ContainerBuilder();
            ConfigurationConfigurator.RegisterConfigurationSettings()
                           .FromAssemblies(typeof(Program).Assembly)
                           .RegisterWithContainer(configSetting => containerBuilder.RegisterInstance(configSetting)
                                                                          .AsSelf()
                                                                          .SingleInstance())
                           .DoYourThing();
            containerBuilder.RegisterType<BusConfiguration>().SingleInstance().AsImplementedInterfaces();
            containerBuilder.Register(container =>
            {
                var busConfig = container.Resolve<IBusConfiguration>();
                var mediatorBuilder = new MediatorBuilder();
                return mediatorBuilder.RegisterHandlers(typeof(Program).Assembly)
                    .ConfigurePublishPipe(x =>
                    {
                        x.UseMessageQueue(() => busConfig,
                        () => true);
                    }).Build();
            }).AsImplementedInterfaces();
            
            
            using (var container = containerBuilder.Build())
            {
                var mediator = container.Resolve<IMediator>();
                await mediator.SendAsync(new CalculateTotalCommand(1, 2));
            }
        }
    }
}
