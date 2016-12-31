using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQueryDispatcher
    {
        Task<IEnumerable<TResult>> QueryAsync<TResult>()
            where TResult : class;

        Task<IEnumerable<TResult>> QueryAsync<TQuery, TResult>()
            where TQuery : IQuery
            where TResult : class;

        Task<IEnumerable<TResult>> QueryAsync<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery
            where TResult : class ;

        Task<TResult> QuerySingleOrDefaultAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : class;

        IEnumerable<TResult> Query<TResult>()
            where TResult : class;

        IEnumerable<TResult> Query<TQuery, TResult>()
            where TQuery : IQuery
            where TResult : class;

        IEnumerable<TResult> Query<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : class;

        TResult QuerySingleOrDefault<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : class;

    }
}
