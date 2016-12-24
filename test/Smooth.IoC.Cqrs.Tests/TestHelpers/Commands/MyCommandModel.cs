using Smooth.IoC.Cqrs.Commanding;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Commands
{
    public class MyCommandModel : Command
    {
        public int Value { get; set; }
    }
}
