using System;

namespace Smooth.IoC.Cqrs.Requests
{
    /// <summary>
    /// A request is the first part of the request/reply pattern. 
    /// </summary>
    /// <remarks>
    /// This interface is required so that we can get the request id in a non-generic way.
    /// </remarks>
    /// <seealso cref="Request"/>
    public interface IRequest
    {
        /// <summary>
        /// Get id identifying this request (so that we know when we get the correct reply).
        /// </summary>
        Guid RequestId { get; }
        int Version { get; }
    }
}
