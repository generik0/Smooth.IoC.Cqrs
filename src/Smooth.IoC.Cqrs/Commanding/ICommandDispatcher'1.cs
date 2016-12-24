using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Commanding
{
    public interface ICommandDispatcher
    {
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}