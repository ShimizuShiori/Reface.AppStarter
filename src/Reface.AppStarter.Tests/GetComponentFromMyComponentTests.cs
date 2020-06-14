using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Tests.MyComponents;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class GetComponentFromMyComponentTests
    {
        [TestMethod]
        public void Get()
        {
            var app = new AppSetup().Start(new TestAppModule());
            using (var work = app.BeginWork("T"))
            {
                var aaa = work.CreateComponent<AAA>();
                Assert.IsNotNull(aaa);
            }
        }
    }
}
