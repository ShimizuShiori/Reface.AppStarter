using ClassLibrary1.Services;
using Mail;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;

namespace ClassLibrary1
{
    [ComponentScanAppModule]
    [MailAppModule]
    public class CL1AppModule : AppModule
    {
        [ReplaceCreator]
        public IMailSender GetMailSender()
        {
            return new MailService();
        }
    }
}
