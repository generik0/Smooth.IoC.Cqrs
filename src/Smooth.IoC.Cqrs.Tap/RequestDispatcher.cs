using System;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Exceptions;
using Smooth.IoC.Cqrs.Requests;

namespace Smooth.IoC.Cqrs.Tap
{
    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly IHandlerFactory _factory;
        public RequestDispatcher(IHandlerFactory factory)
        {
            _factory = factory;
        }

        public IRequestHandler<TRequest, TReply> GetRequestHandler<TRequest, TReply>()
            where TRequest : IRequest
            where TReply : IComparable
        {
            return _factory.ResolveRequest<TRequest, TReply>();
        }

        public IRequestHandler<TReply> GetRequestHandler<TReply>()
            where TReply : IComparable
        {
            return _factory.ResolveRequest<TReply>();
        }

        public Task<TReply> ExecuteAsync<TRequest, TReply>(TRequest request)
            where TRequest : IRequest
            where TReply : IComparable 
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            using (var handler = GetRequestHandler<TRequest, TReply>())
            {
                if (handler == null)
                {
                    throw new RequestHandlerNotFoundException(typeof(TRequest));
                }
                return handler.ExecuteAsync(request);
            }
        }

        public TReply Execute<TRequest, TReply>(TRequest request) where TRequest : IRequest where TReply : IComparable
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            using (var handler = GetRequestHandler<TRequest, TReply>())
            {
                if (handler == null)
                {
                    throw new RequestHandlerNotFoundException(typeof(TRequest));
                }
                return handler.Execute(request);
            }
        }

        public Task<TReply> ExecuteAsync<TReply>() where TReply : IComparable
        {
            using (var handler = GetRequestHandler<TReply>())
            {
                if (handler == null)
                {
                    throw new RequestHandlerNotFoundException("ExecuteAsync failed");
                }
                return handler.ExecuteAsync();
            }
        }

        public TReply Execute<TReply>() where TReply : IComparable
        {
            using (var handler = GetRequestHandler<TReply>())
            {
                if (handler == null)
                {
                    throw new RequestHandlerNotFoundException("ExecuteAsync failed");
                }
                return handler.Execute();
            }
        }
    }
}
