using System;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Commanding
{
    public interface ICommandHandler<in TCommand> : IHandle
        where TCommand : ICommand
    {
        /// <summary>
        /// Execute a command asynchronously.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <returns>Task which will be completed once the command has been executed.</returns>
        Task ExecuteAsync(TCommand command);
    }
}
