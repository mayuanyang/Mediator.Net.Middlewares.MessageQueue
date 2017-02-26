using Mediator.Net.Contracts;

namespace Mediator.Net.Middlewares.MessageQueue.Console.Commands
{
    class CalculateTotalCommand : ICommand
    {
        public decimal Arg1 { get; }
        public decimal Arg2 { get; }

        public CalculateTotalCommand(decimal arg1, decimal arg2)
        {
            Arg1 = arg1;
            Arg2 = arg2;
        }
    }
}
