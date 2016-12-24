using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Commanding;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Commands
{
    public class MyCommandDispatcher : ICommandDispatcher<MyCommandModel>
        
    {
        private readonly ICommandDispatcher _dispatcher;
        
        public MyCommandDispatcher(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task ExecuteAsync(MyCommandModel command)
        {
            //Can do anything. But here jsut call the general dispatcher
            return _dispatcher.ExecuteAsync(command);
        }

        public void Execute(MyCommandModel command)
        {
            ExecuteAsync(command).RunSynchronously();
        }
    }
}
