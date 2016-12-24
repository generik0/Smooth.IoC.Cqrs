using System;

namespace Smooth.IoC.Cqrs
{
    public interface IHandler : IDisposable
    {
        bool IsHandel<THandle>() where THandle : class;
    }
}