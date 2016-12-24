using System;
using System.Collections.Generic;
using System.Linq;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Commands;

namespace Smooth.IoC.Cqrs.Tests
{
    public sealed class Brave : IBrave
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly ICommandDispatcher<MyCommandModel> _specialDispatcher;
        private readonly IEnumerable<IHandler> _handlers;
        private readonly ICommandHandler<MyCommandModel> _handler;

            public Brave(ICommandDispatcher dispatcher, ICommandDispatcher<MyCommandModel> specialDispatcher, IEnumerable<IHandler> handlers, 
                ICommandHandler<MyCommandModel> handler   )
        {
            _dispatcher = dispatcher;
            _specialDispatcher = specialDispatcher;
            _handlers = handlers;
            _handler = handler;
        }

        public async void DoDispatch(MyCommandModel cmd)
        {
            await _dispatcher.ExecuteAsync(cmd);
        }

        public async void DoSpecialDispatch(MyCommandModel cmd)
        {
            await _specialDispatcher.ExecuteAsync(cmd);
        }

        public async void DoDecoratorDispatch(MyCommandModel cmd)
        {
            var decorate = _handlers.FirstOrDefault(x => x.IsHandel<MyCommandHandler>()) as MyCommandHandler;
            if (decorate == null) throw new NullReferenceException("MyCommandHandler is null");
            await decorate.ExecuteAsync(cmd);
        }

        public async void DoExactHandler(MyCommandModel cmd)
        {
            await _handler.ExecuteAsync(cmd);
        }
    }
}
