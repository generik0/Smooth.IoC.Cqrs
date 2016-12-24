using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Commanding
{
    public interface ICommandDispatcher
    {
        Task ExecuteAsync<TCommand>(TCommand request) where TCommand : ICommand;
    }
}