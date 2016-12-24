﻿using System;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Tasks.Exceptions;

namespace Smooth.IoC.Cqrs.Tasks.Requests
{
    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly IHandlerFactory _factory;
        public RequestDispatcher(IHandlerFactory factory)
        {
            _factory = factory;
        }

        public Task<TReply> ExecuteAsync<TRequest, TReply>(TRequest request)
            where TRequest : IRequest
            where TReply : class 
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            using (var handler = _factory.ResolveRequest <TRequest, TReply>())
            {
                if (handler == null)
                {
                    throw new RequestHandlerNotFoundException(typeof(TRequest));
                }
                return handler.ExecuteAsync(request);
            }
        }
    }
}
