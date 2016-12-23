using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Commanding;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Commands
{
    public class MyCommandHandler : Handler, ICommandHandler<MyCommandModel>
    {
        public MyCommandHandler(IHandlerFactory handlerFactory) : base(handlerFactory)
        {
        }

        public Task ExecuteAsync(MyCommandModel command)
        {
            return Task.FromResult(command.Value++);
        }
    }
}
