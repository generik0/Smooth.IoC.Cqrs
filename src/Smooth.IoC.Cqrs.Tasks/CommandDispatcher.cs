using System;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Exceptions;

namespace Smooth.IoC.Cqrs.Tasks
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IHandlerFactory _factory;

        public CommandDispatcher(IHandlerFactory factory)
        {
            _factory = factory;
        }

        public Task ExecuteAsync<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            using (var handler = _factory.Resolve<TCommand>())
            {
                if (handler == null)
                {
                    throw new CommandHandlerNotFoundException(typeof(TCommand));
                }

                return handler.ExecuteAsync(command);
            }
        }
    }
}
