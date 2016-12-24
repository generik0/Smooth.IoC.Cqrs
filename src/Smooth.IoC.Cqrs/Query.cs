using System;

namespace Smooth.IoC.Cqrs
{
    public abstract class Query : IQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class.
        /// </summary>
        protected Query()
        {
            QueryId = Guid.NewGuid();
        }

        protected Query(int version)
        {
            Version = version;
            QueryId = Guid.NewGuid();
        }

        protected Query(Guid queryId)
        {
            QueryId = queryId;
        }

        protected Query(Guid queryId, int version)
        {
            QueryId = queryId;
            Version = version;
        }

        /// <summary>
        /// Gets unique identifier for this request (so that we can identify replies).
        /// </summary>
        public Guid QueryId { get; }
        public int Version { get; }
    }

}
