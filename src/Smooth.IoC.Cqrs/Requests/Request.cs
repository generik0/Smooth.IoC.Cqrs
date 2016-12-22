using System;

namespace Smooth.IoC.Cqrs.Requests
{
    public class Request : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        public Request()
        {
            RequestId = Guid.NewGuid();
        }

        /// <summary>
        /// Gets unique identifier for this request (so that we can identify replies).
        /// </summary>
        public Guid RequestId { get; }
    }

}
