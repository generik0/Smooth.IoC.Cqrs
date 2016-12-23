using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Queries;

namespace Smooth.IoC.Cqrs.Tests
{
    public interface IWorld
    {
        Task<MyResultModel> DoDecoratorDispatch(MyQueryModel query);
        Task<MyResultModel> DoDispatch(MyQueryModel query);
        Task<MyResultModel> DoExactHandler(MyQueryModel query);
        Task<MyResultModel> DoSpecialDispatch(MyQueryModel query);
    }
}