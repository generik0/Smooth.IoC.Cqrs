using Smooth.IoC.Cqrs.Tasks.Commanding;
using Smooth.IoC.Cqrs.Tasks.Query;
using Smooth.IoC.Cqrs.Tasks.Requests;

namespace Smooth.IoC.Cqrs.Tasks
{
    public interface ICqrsDispatcher : ICommandDispatcher, IRequestDispatcher, IQueryDispatcher
    {
        
    }

}
