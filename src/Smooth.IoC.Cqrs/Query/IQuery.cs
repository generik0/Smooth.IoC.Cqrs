using System;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQuery 
    {
        /// <summary>
        /// Get id identifying this request (so that we know when we get the correct reply).
        /// </summary>
        Guid QueryId { get; }

        int Version { get; }
    }
}
