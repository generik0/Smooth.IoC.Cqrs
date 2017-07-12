﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Exceptions;
using Smooth.IoC.Cqrs.Query;

namespace Smooth.IoC.Cqrs.Tap
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IHandlerFactory _factory;

        public QueryDispatcher(IHandlerFactory factory)
        {
            _factory = factory;
        }

        public Task<IEnumerable<TResult>> QueryAsync<TResult>() where TResult : class
        {
            using (var handler = _factory.ResolveQuery< TResult>())
            {
                if (handler == null)
                {
                    throw new QueryHandlerNotFoundException("QueryAsync failed");
                }
                return handler.QueryAsync();
            }
        }

        public Task<IEnumerable<TResult>> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            using (var handler = _factory.ResolveQuery<TQuery, TResult>())
            {
                if (handler == null)
                {
                    throw new QueryHandlerNotFoundException(typeof(TQuery));
                }
                return handler.QueryAsync(query);
            }
        }

        public Task<TResult> QuerySingleOrDefaultAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            using (var handler = _factory.ResolveSingleQuery<TQuery, TResult>())
            {
                if (handler == null)
                {
                    throw new QueryHandlerNotFoundException(typeof(TQuery));
                }
                return handler.QuerySingleOrDefaultAsync(query);
            }
        }

        public IEnumerable<TResult> Query<TResult>() where TResult : class
        {
            using (var handler = _factory.ResolveQuery<TResult>())
            {
                if (handler == null)
                {
                    throw new QueryHandlerNotFoundException("QueryAsync failed");
                }
                return handler.Query();
            }
        }

        public IEnumerable<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            using (var handler = _factory.ResolveQuery<TQuery, TResult>())
            {
                if (handler == null)
                {
                    throw new QueryHandlerNotFoundException(typeof(TQuery));
                }
                return handler.Query(query);
            }
        }

        public TResult QuerySingleOrDefault<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            using (var handler = _factory.ResolveSingleQuery<TQuery, TResult>())
            {
                if (handler == null)
                {
                    throw new QueryHandlerNotFoundException(typeof(TQuery));
                }
                return handler.QuerySingleOrDefault(query);
            }
        }

        public TResult QuerySingleOrDefault<TResult>()  where TResult : class
        {
            using (var handler = _factory.ResolveSingleQuery<TResult>())
            {
                if (handler == null)
                {
                    throw new QueryHandlerNotFoundException("QueryAsync failed");
                }
                return handler.QuerySingleOrDefault();
            }
        }
    }
}
