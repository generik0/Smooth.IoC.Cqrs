using System;

namespace Smooth.IoC.Cqrs.Tasks
{
    public interface IHandler : IDisposable
    {
        bool IsHandel<THandle>() where THandle : class;
    }
}