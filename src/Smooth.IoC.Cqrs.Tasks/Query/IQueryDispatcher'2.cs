using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Tasks.Query
{
    public interface IQueryDispatcher<in TQuery, TResult>
        where TQuery : Cqrs.Query
        where TResult : class
    {
        Task<IEnumerable<TResult>> QueryAsync();
        Task<IEnumerable<TResult>> QueryAsync(TQuery query);
        Task<TResult> QuerySingleOrDefaultAsync(TQuery query);
    }
}
