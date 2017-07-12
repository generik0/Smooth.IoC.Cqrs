using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Commanding
{
    public interface ICommandDispatcher
    {
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;
        ICommandHandler<TCommand> GetCommandHandler<TCommand>() where TCommand : ICommand;
    }
}