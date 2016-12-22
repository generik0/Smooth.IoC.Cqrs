using System;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Exceptions;

namespace Smooth.IoC.Cqrs.Commanding
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IHandlerFactory _factory;

        public CommandDispatcher(IHandlerFactory factory)
        {
            _factory = factory;
        }

        public Task ExecuteAsync<TCommand>(TCommand request)
            where TCommand : ICommand
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            using (var handler = _factory.Resolve<TCommand>())
            {
                if (handler == null)
                {
                    throw new CommandHandlerNotFoundException(typeof(TCommand));
                }

                return handler.ExecuteAsync(request);
            }
        }
    }
}
