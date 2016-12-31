using System;
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
                    throw new RequestHandlerNotFoundException("QueryAsync failed");
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
                    throw new RequestHandlerNotFoundException(typeof(TQuery));
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
                    throw new RequestHandlerNotFoundException(typeof(TQuery));
                }
                return handler.QuerySingleOrDefaultAsync(query);
            }
        }

        public IEnumerable<TResult> Query<TResult>() where TResult : class
        {
            return QueryAsync<TResult>().Result;
        }

        public IEnumerable<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            return QueryAsync<TQuery, TResult>(query).Result;
        }

        public TResult QuerySingleOrDefault<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            return QuerySingleOrDefaultAsync<TQuery, TResult>(query).Result;
        }
    }
}
