using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Tests.AppContainerBuilders;
using Reface.AppStarter.Tests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.Tests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class RegisteAsFunTest
    {
        [TestMethod]
        public void Test()
        {
            AppSetup appSetup = new AppSetup();
            TestAppModule testAppModule = new TestAppModule();
            appSetup.GetAppContainerBuilder<TestContainerBuilder>();
            var app = appSetup.Start(testAppModule);
            var container = app.GetAppContainer<IComponentContainer>();
            using (var scope = container.BeginScope("test"))
            {
                var t = scope.CreateComponent<IService>();
                Assert.IsInstanceOfType(t, typeof(ServiceRegistedInTestContainerBuilder));
            }
        }
    }
}
