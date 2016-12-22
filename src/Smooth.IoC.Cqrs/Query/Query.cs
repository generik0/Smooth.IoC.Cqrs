using System;

namespace Smooth.IoC.Cqrs.Query
{
    public class Query : IQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class.
        /// </summary>
        public Query()
        {
            RequestId = Guid.NewGuid();
        }

        /// <summary>
        /// Gets unique identifier for this request (so that we can identify replies).
        /// </summary>
        public Guid RequestId { get; }
    }

}
