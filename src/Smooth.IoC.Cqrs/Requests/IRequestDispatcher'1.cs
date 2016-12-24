using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Requests
{
    public interface IRequestDispatcher
    {
        Task<TReply> ExecuteAsync<TRequest, TReply>(TRequest request) 
            where TRequest : IRequest
            where TReply : class ;

        TReply Execute<TRequest, TReply>(TRequest request)
            where TRequest : IRequest
            where TReply : class;
    }
}
