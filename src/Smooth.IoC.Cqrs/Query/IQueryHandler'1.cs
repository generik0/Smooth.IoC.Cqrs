using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQueryHandler<in TQuery, TResult> : IDisposable 
        where TQuery : IQuery
        where TResult : class
    {
        /// <summary>
        /// Execute a query asynchronously.
        /// </summary>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        Task<IEnumerable<TResult>> QueryAsync();

        /// <summary>
        /// Execute a query asynchronously.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        Task<IEnumerable<TResult>> QueryAsync(TQuery query);

        /// <summary>
        /// Execute a query asynchronously.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        Task<TResult> QuerySingleOrDefaultAsync(TQuery query);

        /// <summary>
        /// Execute a query synchronously.
        /// </summary>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        IEnumerable<TResult> Query();

        /// <summary>
        /// Execute a query synchronously.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        IEnumerable<TResult> Query(TQuery query);

        /// <summary>
        /// Execute a query synchronously.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        TResult QuerySingleOrDefault(TQuery query);
    }
}
