using System;

namespace Smooth.IoC.Cqrs.Commanding
{
    public class Command : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public Command()
        {
            CommandId = Guid.NewGuid();
        }

        /// <summary>
        /// Gets unique identifier for this request (so that we can identify replies).
        /// </summary>
        public Guid CommandId { get; }
    }

}
