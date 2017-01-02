using System;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Exceptions;

namespace Smooth.IoC.Cqrs.Tap
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
            using (var handler = _factory.ResolveCommand<TCommand>())
            {
                if (handler == null)
                {
                    throw new CommandHandlerNotFoundException(typeof(TCommand));
                }

                return handler.ExecuteAsync(command);
            }
        }


        public void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            using (var handler = _factory.ResolveCommand<TCommand>())
            {
                if (handler == null)
                {
                    throw new CommandHandlerNotFoundException(typeof(TCommand));
                }
                handler.Execute(command);
            }
        }
    }
}
