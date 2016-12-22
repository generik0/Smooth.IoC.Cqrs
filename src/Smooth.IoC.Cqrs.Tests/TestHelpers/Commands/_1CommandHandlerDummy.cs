using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Commanding;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Commands
{
    public class _1CommandHandlerDummy : Handle, ICommandHandler<_CommandModelDummy>
    {
        public _1CommandHandlerDummy(IHandlerFactory handlerFactory) : base(handlerFactory)
        {
        }

        public Task ExecuteAsync(_CommandModelDummy command)
        {
            return Task.FromResult(command.Value += ++command.Value);
        }
    }
}
