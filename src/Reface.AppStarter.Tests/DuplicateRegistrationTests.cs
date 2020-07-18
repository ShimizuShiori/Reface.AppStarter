using ClassLibrary2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Tests.AppModules;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class DuplicateRegistrationTests
    {
        [TestMethod]
        public void Svr2ShouldOnlyOne()
        {
            var app = AppSetup.Start<C1AndC2AppModule>();
            var svrs = app.CreateComponent<IEnumerable<ISvr2>>();
            Assert.AreEqual(1, svrs.Count());
        }
    }
}
