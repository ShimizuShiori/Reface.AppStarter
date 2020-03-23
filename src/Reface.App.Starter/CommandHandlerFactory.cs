using Autofac;
using Reface.AppStarter.Attributes;
using Reface.CommandBus;
using Reface.CommandBus.Errors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter
{
    [Component]
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly ILifetimeScope lifetimeScope;
        private readonly Type commandHandlerBareTtypetypeof;
        public CommandHandlerFactory(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
            this.commandHandlerBareTtypetypeof = typeof(ICommandHandler<>);
        }
        public ICommandHandler<TCommand> Create<TCommand>() where TCommand : ICommand
        {
            Type requiredHandlerCommandType = this.commandHandlerBareTtypetypeof.MakeGenericType(typeof(TCommand));
            IEnumerable<ICommandHandler> commandHandlers = this.lifetimeScope.Resolve<IEnumerable<ICommandHandler>>();
            var result = commandHandlers
                .FirstOrDefault(x => requiredHandlerCommandType.IsAssignableFrom(x.GetType()));

            if (result == null)
                throw new CommandHandlerNotFoundException(typeof(TCommand));

            return (ICommandHandler<TCommand>)result;
        }
    }
}
