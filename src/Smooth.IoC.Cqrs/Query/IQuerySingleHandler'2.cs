using System;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQuerySingleHandler<TResult> : IDisposable 
        where TResult : class
    {
        /// <summary>
        /// Execute a query asynchronously.
        /// </summary>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        Task<TResult> QuerySingleOrDefaultAsync();

        /// <summary>
        /// Execute a query synchronously.
        /// </summary>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        TResult QuerySingleOrDefault();
    }
}
