using System;

namespace Smooth.IoC.Cqrs.Tasks.Exceptions
{
    public class QueryHandlerNotFoundException : Exception
    {
        public QueryHandlerNotFoundException(Type type) : base($"Could not create command handler of type: {type.Name}") {}
    }
}
