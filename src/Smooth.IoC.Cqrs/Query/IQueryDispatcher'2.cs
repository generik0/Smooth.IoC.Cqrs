using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQueryDispatcher<in TQuery, TResult>
        where TQuery : Query
        where TResult : class
    {
        Task<IEnumerable<TResult>> QueryAsync();
        Task<IEnumerable<TResult>> QueryAsync(TQuery query);
        Task<TResult> QuerySingleOrDefaultAsync(TQuery query);
        IEnumerable<TResult> Query();
        IEnumerable<TResult> Query(TQuery query);
        TResult QuerySingleOrDefault(TQuery query);
        
    }
}
