using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public class QueryDispatcher<TQuery, TResult> : IQueryDispatcher<TQuery, TResult>
        where TQuery : IQuery 
        where TResult : class
    {
        private readonly IQueryDispatcher _dispatcher;

        public QueryDispatcher(IQueryDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task<IReadOnlyCollection<TResult>> QueryAsync()
        {
            return _dispatcher.QueryAsync<TQuery, TResult>();
        }

        public Task<IReadOnlyCollection<TResult>> QueryAsync(TQuery query)
        {
            return _dispatcher.QueryAsync<TQuery, TResult>(query);
        }

        public Task<TResult> QuerySingleOrDefaultAsync(TQuery query)
        {
            return _dispatcher.QuerySingleOrDefaultAsync<TQuery, TResult>(query);
        }
    }
}
