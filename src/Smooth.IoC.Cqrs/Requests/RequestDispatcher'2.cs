using System;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Exceptions;

namespace Smooth.IoC.Cqrs.Requests
{
    public class RequestDispatcher<TRequest, TReply> : IRequestDispatcher<TRequest, TReply>
        where TRequest : IRequest
        where TReply : class
    {
        private readonly IRequestDispatcher _dispatcher;

        public RequestDispatcher(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task<TReply> ExecuteAsync(TRequest request)
        {
            return _dispatcher.ExecuteAsync<TRequest,TReply>(request);
        }
    }
}
