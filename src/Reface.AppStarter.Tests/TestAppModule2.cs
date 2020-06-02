using ClassLibrary1;
using ClassLibrary1.Services;
using Mail;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Tests.Services;
using System.Diagnostics;

namespace Reface.AppStarter.Tests
{
    [AutoConfigAppModule]
    [ComponentScanAppModule]
    [CL1AppModule]
    [MailAppModule]
    public class TestAppModule2 : AppModule
    {
        public override void OnUsing(AppModuleUsingArguments arguments)
        {
            //var container = setup.GetAppContainerBuilder<TestContainerBuilder>();
            //container.DoNothing();
        }

        [ComponentCreator]
        public ServiceRegistedByAppModule GetServiceRegistedByAppModule()
        {
            return new DefaultServiceRegistedByAppModule();
        }

        [ReplaceCreator]
        public ICL1Service GetCL1Service()
        {
            return new MyDefaultCL1Service();
        }

        [ReplaceCreator]
        public IMailSender GetMailSender()
        {
            return new TestMailSender();
        }

        class TestMailSender : IMailSender
        {
            public void Send(string address, string title, string content)
            {
                Debug.WriteLine("TestMailSender");
            }
        }
    }
}
