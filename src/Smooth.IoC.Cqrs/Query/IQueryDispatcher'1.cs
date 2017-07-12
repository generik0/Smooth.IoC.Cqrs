using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQueryDispatcher
    {
        Task<IEnumerable<TResult>> QueryAsync<TResult>()
            where TResult : class;

        Task<IEnumerable<TResult>> QueryAsync<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery
            where TResult : class ;

        Task<TResult> QuerySingleOrDefaultAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : class;

        IEnumerable<TResult> Query<TResult>()
            where TResult : class;

        IEnumerable<TResult> Query<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : class;

        TResult QuerySingleOrDefault<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : class;

        TResult QuerySingleOrDefault<TResult>()
            where TResult : class;

        IQueryHandler<TResult> GetQueryHandler<TResult>()
            where TResult : class;

        IQueryHandler<TQuery, TResult> GetQueryHandler<TQuery, TResult>()
            where TQuery : IQuery where TResult : class;

        IQuerySingleHandler<TResult> GetSingleOrDefaultQueryHandler<TResult>()
            where TResult : class;

        IQuerySingleHandler<TQuery,TResult> GetSingleOrDefaultQueryHandler<TQuery, TResult>()
            where TQuery : IQuery where TResult : class;

    }
}
