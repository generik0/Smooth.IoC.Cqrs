using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQueryDispatcher<TResult>
        where TResult : class
    {
        Task<IEnumerable<TResult>> QueryAsync();
        IEnumerable<TResult> Query();
    }
}
