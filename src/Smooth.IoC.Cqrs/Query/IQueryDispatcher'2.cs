using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQueryDispatcher<in TQuery, TResult>
        where TQuery : IQuery
        where TResult : class
    {
        Task<IReadOnlyCollection<TResult>> QueryAsync();

        Task<IReadOnlyCollection<TResult>> QueryAsync(TQuery query);

        Task<TResult> QuerySingleOrDefaultAsync(TQuery query);
    }
}
