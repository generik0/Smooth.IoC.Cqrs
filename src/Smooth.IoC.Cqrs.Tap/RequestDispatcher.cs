﻿using System;
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

        public Task<TReply> ExecuteAsync<TRequest, TReply>(TRequest request)
            where TRequest : IRequest
            where TReply : IComparable 
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            using (var handler = _factory.ResolveRequest <TRequest, TReply>())
            {
                if (handler == null)
                {
                    throw new HandlerNotFoundException(typeof(TRequest));
                }
                return handler.ExecuteAsync(request);
            }
        }

        public TReply Execute<TRequest, TReply>(TRequest request) where TRequest : IRequest where TReply : IComparable
        {
            return ExecuteAsync<TRequest, TReply>(request).Result;
        }

        public Task<TReply> ExecuteAsync<TReply>() where TReply : IComparable
        {
            using (var handler = _factory.ResolveRequest<TReply>())
            {
                if (handler == null)
                {
                    throw new HandlerNotFoundException("ExecuteAsync failed");
                }
                return handler.ExecuteAsync();
            }
        }

        public TReply Execute<TReply>() where TReply : IComparable
        {
            return ExecuteAsync<TReply>().Result;
        }
    }
}
