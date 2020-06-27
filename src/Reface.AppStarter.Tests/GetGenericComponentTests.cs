using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Tests.Entities;
using Reface.AppStarter.Tests.Repositories;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class GetGenericComponentTests
    {
        [TestMethod]
        public void TestGetTwoRepository()
        {
            var app = AppSetup.Start<TestAppModule>();
            using (var work = app.BeginWork(""))
            {
                IRepository<EntityA> eas = work.CreateComponent<IRepository<EntityA>>();
                IRepository<EntityB> ebs = work.CreateComponent<IRepository<EntityB>>();
            }
        }
    }
}
