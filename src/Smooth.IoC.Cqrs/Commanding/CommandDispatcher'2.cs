using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Commanding
{
    public class CommandDispatcher<TCommand> : ICommandDispatcher<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandDispatcher _dispatcher;
        
        public CommandDispatcher(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task ExecuteAsync(TCommand command)
        {
            return _dispatcher.ExecuteAsync(command);
        }
    }
}
