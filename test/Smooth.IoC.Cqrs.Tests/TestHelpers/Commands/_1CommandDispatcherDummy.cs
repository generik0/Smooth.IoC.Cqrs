using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Commanding;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Commands
{
    public class _1CommandDispatcherDummy : ICommandDispatcher<_CommandModelDummy>
    {
        private readonly ICommandDispatcher _dispatcher;
        
        public _1CommandDispatcherDummy(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task ExecuteAsync(_CommandModelDummy command)
        {
            //Can do anything. But here just call the general dispatcher
            return _dispatcher.ExecuteAsync(command);
        }

        public void Execute(_CommandModelDummy command)
        {
            ExecuteAsync(command).RunSynchronously();
        }
    }
}
