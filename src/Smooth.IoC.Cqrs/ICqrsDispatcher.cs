using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Query;
using Smooth.IoC.Cqrs.Requests;

namespace Smooth.IoC.Cqrs
{
    public interface ICqrsDispatcher : ICommandDispatcher, IRequestDispatcher, IQueryDispatcher
    {
        
    }

}
