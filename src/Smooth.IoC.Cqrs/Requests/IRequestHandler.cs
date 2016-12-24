using System;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Requests
{
    public interface IRequestHandler<in TRequest, TReply> : IDisposable 
        where TRequest : IRequest
        where TReply : class
    {
        /// <summary>
        /// Execute a request asynchronously.
        /// </summary>
        /// <param name="request">Command to execute.</param>
        /// <returns>Task which will be completed once the command has been executed.</returns>
        Task<TReply> ExecuteAsync(TRequest request);

        /// <summary>
        /// Execute a request synchronously.
        /// </summary>
        /// <param name="request">Command to execute.</param>
        /// <returns>Task which will be completed once the command has been executed.</returns>
        TReply Execute(TRequest request);
    }
}
