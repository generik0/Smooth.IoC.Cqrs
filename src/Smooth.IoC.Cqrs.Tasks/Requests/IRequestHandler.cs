using System;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Tasks.Requests
{
    public interface IRequestHandler<in TRequest, TReply> : IDisposable 
        where TRequest : IRequest
        where TReply : class
    {
        /// <summary>
        /// Execute a command asynchronously.
        /// </summary>
        /// <param name="request">Command to execute.</param>
        /// <returns>Task which will be completed once the command has been executed.</returns>
        Task<TReply> ExecuteAsync(TRequest request);
    }
}
