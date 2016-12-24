using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Tasks.Commanding
{
    public interface ICommandDispatcher<in TCommand> 
        where TCommand : Command
    {
        Task ExecuteAsync(TCommand command) ;
    }
}