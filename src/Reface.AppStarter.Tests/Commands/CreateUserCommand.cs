using Reface.CommandBus;

namespace Reface.AppStarter.Tests.Commands
{
    public class CreateUserCommand : ICommand
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public CreateUserCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
