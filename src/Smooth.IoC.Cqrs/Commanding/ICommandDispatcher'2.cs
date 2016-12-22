using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Commanding
{
    public interface ICommandDispatcher<in TCommand> 
        where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command) ;
    }
}