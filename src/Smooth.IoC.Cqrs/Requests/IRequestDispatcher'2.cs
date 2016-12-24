using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Requests
{
    public interface IRequestDispatcher<in TRequest, TReply>
        where TRequest : Request
        where TReply : class
    {
        Task<TReply> ExecuteAsync(TRequest request);
    }
}
