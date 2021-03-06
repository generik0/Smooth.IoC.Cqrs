﻿using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Commanding
{
    public interface ICommandDispatcher<in TCommand> 
        where TCommand : Command
    {
        Task ExecuteAsync(TCommand command) ;
        void Execute(TCommand command);
    }
}