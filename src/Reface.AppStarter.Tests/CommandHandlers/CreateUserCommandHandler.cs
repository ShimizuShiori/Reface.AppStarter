using Reface.AppStarter.Attributes;
using Reface.AppStarter.Tests.Commands;
using Reface.CommandBus;

namespace Reface.AppStarter.Tests.CommandHandlers
{
    [CommandHandler]
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        public object Handler(CreateUserCommand command)
        {
            return "1234";
        }
    }
}
