using System;

namespace Smooth.IoC.Cqrs.Commanding
{
    /// <summary>
    /// A request is the first part of the command pattern. 
    /// </summary>
    /// <remarks>
    /// This interface is required so that we can get the request id in a non-generic way.
    /// </remarks>
    /// <seealso cref="¨Command"/>
    public interface ICommand
    {
        /// <summary>
        /// Get id identifying this request (so that we know when we get the correct reply).
        /// </summary>
        Guid CommandId { get; }
    }
}
