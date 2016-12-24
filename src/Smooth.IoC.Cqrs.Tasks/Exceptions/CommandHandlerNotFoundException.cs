using System;

namespace Smooth.IoC.Cqrs.Tasks.Exceptions
{
    public class CommandHandlerNotFoundException : Exception
    {
        public CommandHandlerNotFoundException(Type type) : base($"Could not create command handler of type: {type.Name}") {}
    }
}
