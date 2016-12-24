using System;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Requests
{
    public interface IRequestDispatcher<in TRequest, TReply>
        where TRequest : Request
        where TReply : IComparable
    {
        Task<TReply> ExecuteAsync(TRequest request);

        TReply Execute(TRequest request);
    }
   
}
