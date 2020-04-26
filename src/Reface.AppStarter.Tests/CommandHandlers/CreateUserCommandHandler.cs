using Reface.AppStarter.Attributes;
using Reface.AppStarter.Tests.Commands;
using Reface.CommandBus;

namespace Reface.AppStarter.Tests.CommandHandlers
{
    [CommandHandler]
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        public void Handle(CreateUserCommand command)
        {
            command.CreateResult = "1234";
        }
    }
}
