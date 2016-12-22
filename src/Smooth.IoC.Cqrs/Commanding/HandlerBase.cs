using System;

namespace Smooth.IoC.Cqrs.Commanding
{
    public abstract class HandlerBase :  IDisposable
    {
        private readonly IHandlerFactory _handlerFactory;
        protected bool Disposed;

        protected HandlerBase(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        ~HandlerBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Disposed || !disposing) return;
            Disposed = true;
            _handlerFactory.Release(this);
        }
    }
}
