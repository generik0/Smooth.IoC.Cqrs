using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQueryDispatcher
    {
        Task<IReadOnlyCollection<TResult>> QueryAsync<TQuery, TResult>()
            where TQuery : IQuery
            where TResult : class;

        Task<IReadOnlyCollection<TResult>> QueryAsync<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery
            where TResult : class ;

        Task<TResult> QuerySingleOrDefaultAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : class;
    }
}
