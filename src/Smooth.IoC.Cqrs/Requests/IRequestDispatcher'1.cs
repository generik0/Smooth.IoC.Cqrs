using System;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Requests
{
    public interface IRequestDispatcher
    {
        Task<TReply> ExecuteAsync<TRequest, TReply>(TRequest request) 
            where TRequest : IRequest
            where TReply : IComparable ;

        TReply Execute<TRequest, TReply>(TRequest request)
            where TRequest : IRequest
            where TReply : IComparable;

        Task<TReply> ExecuteAsync<TReply>()
            where TReply : IComparable;

        TReply Execute<TReply>()
            where TReply : IComparable;

        IRequestHandler<TRequest, TReply> GetRequestHandler<TRequest, TReply>()
            where TRequest : IRequest
            where TReply : IComparable;

        IRequestHandler<TReply> GetRequestHandler<TReply>()
            where TReply : IComparable;
    }
}
