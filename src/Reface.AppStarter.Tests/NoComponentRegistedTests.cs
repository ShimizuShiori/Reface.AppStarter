using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.Tests
{

    [TestClass]
    public class NoComponentRegistedTests
    {
        public interface ITest
        {

        }


        public class Test : ITest
        { }

        public interface ITest2 { }

        [TestMethod]
        public void RegisterComponentWhenNoRegited()
        {
            IAppModule appModule = new TestAppModule();
            AppSetup setup = new AppSetup();
            var app = setup.Start(appModule);
            var container = app.GetAppContainer<IComponentContainer>();
            container.NoComponentRegisted += (sender, e) =>
            {
                if (e.ServiceType == typeof(ITest))
                    e.ComponentProvider = c => new Test();
            };
            using (var scope = container.BeginScope("TEST"))
            {
                var t = scope.CreateComponent<ITest>();
                Assert.IsNotNull(t);
                Assert.IsInstanceOfType(t, typeof(Test));
                try
                {
                    scope.CreateComponent<ITest2>();
                    Assert.Fail("!");
                }
                catch (Exception)
                {
                    
                }
            }
        }
    }
}
