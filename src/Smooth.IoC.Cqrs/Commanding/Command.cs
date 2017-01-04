using System;

namespace Smooth.IoC.Cqrs.Commanding
{
    public abstract class Command : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        protected Command()
        {
            CommandId = Guid.NewGuid();
        }

        protected Command(Guid commandId)
        {
            CommandId = commandId;
        }

        protected Command(int version)
        {
            Version = version;
            CommandId = Guid.NewGuid();
        }

        protected Command(Guid commandId, int version)
        {
            CommandId = commandId;
            Version = version;
        }

        /// <summary>
        /// Gets unique identifier for this request (so that we can identify replies).
        /// </summary>
        public virtual Guid CommandId { get; }

        public virtual int Version { get; }
    }

}
