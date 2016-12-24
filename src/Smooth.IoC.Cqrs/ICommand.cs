using System;

namespace Smooth.IoC.Cqrs
{
    public interface ICommand
    {
        /// <summary>
        /// Get id identifying this request (so that we know when we get the correct reply).
        /// </summary>
        Guid CommandId { get; }

        int Version { get; }
    }
}
