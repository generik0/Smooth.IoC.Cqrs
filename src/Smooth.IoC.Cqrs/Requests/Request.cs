﻿using System;

namespace Smooth.IoC.Cqrs.Requests
{
    public abstract class Request : IRequest
    {
        protected Request()
        {
            RequestId = Guid.NewGuid();
        }

        protected Request(int version)
        {
            Version = version;
            RequestId = Guid.NewGuid();
        }

        protected Request(Guid requestId)
        {
            RequestId = requestId;
        }

        protected Request(Guid requestId, int version)
        {
            RequestId = requestId;
            Version = version;
        }

        /// <summary>
        /// Gets unique identifier for this request (so that we can identify replies).
        /// </summary>
        public virtual Guid RequestId { get; }

        public virtual int Version { get; }
    }

}
