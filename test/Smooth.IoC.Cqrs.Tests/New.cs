using System;
using System.Collections.Generic;
using System.Linq;
using Smooth.IoC.Cqrs.Requests;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Requests;

namespace Smooth.IoC.Cqrs.Tests
{
    public class New :INew
    {
        private readonly IRequestDispatcher _dispatcher;
        private readonly IRequestDispatcher<MyRequestModel, MyReplyeEnum> _specialDispatcher;
        private readonly IEnumerable<IHandler> _handlers;
        private readonly IRequestHandler<MyRequestModel, MyReplyeEnum> _handler;

        public New(IRequestDispatcher dispatcher, IRequestDispatcher<MyRequestModel, MyReplyeEnum> specialDispatcher, IEnumerable<IHandler> handlers,
            IRequestHandler<MyRequestModel, MyReplyeEnum> handler)
        {
            _dispatcher = dispatcher;
            _specialDispatcher = specialDispatcher;
            _handlers = handlers;
            _handler = handler;
        }
        public MyReplyeEnum DoDispatch(MyRequestModel request)
        {
            return _dispatcher.ExecuteAsync<MyRequestModel, MyReplyeEnum>(request).Result;
        }
        public MyReplyeEnum DoExactHandler(MyRequestModel request)
        {
            return _specialDispatcher.ExecuteAsync(request).Result;
        }
        public MyReplyeEnum DoSpecialDispatch(MyRequestModel request)
        {
            var decorate = _handlers.FirstOrDefault(x => x.IsHandel<MyRequestHandler>()) as MyRequestHandler;
            if (decorate == null) throw new NullReferenceException("MyRequestHandler is null");
            return decorate.ExecuteAsync(request).Result;
        }
        public MyReplyeEnum DoDecoratorDispatch(MyRequestModel request)
        {
            return _handler.ExecuteAsync(request).Result;
        }
    }
}

