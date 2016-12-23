using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Requests;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Requests;

namespace Smooth.IoC.Cqrs.Tests
{
    public class New :INew
    {
        private readonly IRequestDispatcher _dispatcher;
        private readonly IRequestDispatcher<MyRequestModel, MyReplyModel> _specialDispatcher;
        private readonly IEnumerable<IHandler> _handlers;
        private readonly IRequestHandler<MyRequestModel, MyReplyModel> _handler;

        public New(IRequestDispatcher dispatcher, IRequestDispatcher<MyRequestModel, MyReplyModel> specialDispatcher, IEnumerable<IHandler> handlers,
            IRequestHandler<MyRequestModel, MyReplyModel> handler)
        {
            _dispatcher = dispatcher;
            _specialDispatcher = specialDispatcher;
            _handlers = handlers;
            _handler = handler;
        }
        public MyReplyModel DoDispatch(MyRequestModel request)
        {
            return _dispatcher.ExecuteAsync<MyRequestModel, MyReplyModel>(request).Result;
        }
        public MyReplyModel DoExactHandler(MyRequestModel request)
        {
            return _specialDispatcher.ExecuteAsync(request).Result;
        }
        public MyReplyModel DoSpecialDispatch(MyRequestModel request)
        {
            var decorate = _handlers.FirstOrDefault(x => x.IsHandel<MyRequestHandler>()) as MyRequestHandler;
            if (decorate == null) throw new NullReferenceException("MyRequestHandler is null");
            return decorate.ExecuteAsync(request).Result;
        }
        public MyReplyModel DoDecoratorDispatch(MyRequestModel request)
        {
            return _handler.ExecuteAsync(request).Result;
        }
    }

    public class MyRequestHandler : Handler, IRequestHandler<MyRequestModel,MyReplyModel>
    {
        public MyRequestHandler(IHandlerFactory handlerFactory) : base(handlerFactory)
        {
        }

        public Task<MyReplyModel> ExecuteAsync(MyRequestModel request)
        {            
            return Task.FromResult(new MyReplyModel
            {
                Actual = request.Value +1
            });
        }
    }
}

