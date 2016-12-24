using Smooth.IoC.Cqrs.Tests.TestHelpers.Commands;

namespace Smooth.IoC.Cqrs.Tests
{
    public interface IBrave
    {
        void DoDecoratorDispatch(MyCommandModel cmd);
        void DoDispatch(MyCommandModel cmd);
        void DoExactHandler(MyCommandModel cmd);
        void DoSpecialDispatch(MyCommandModel cmd);
    }
}