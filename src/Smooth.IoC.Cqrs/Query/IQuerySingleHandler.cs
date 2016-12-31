using System;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQuerySingleHandler<in TQuery, TResult> : IDisposable 
        where TQuery : IQuery
        where TResult : class
    {
        /// <summary>
        /// Execute a query asynchronously.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        Task<TResult> QuerySingleOrDefaultAsync(TQuery query);

        /// <summary>
        /// Execute a query synchronously.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        TResult QuerySingleOrDefault(TQuery query);
    }
}
