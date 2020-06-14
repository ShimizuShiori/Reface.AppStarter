using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Predicates;
using System;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class PredicateTests
    {
        [TestMethod]
        public void AndOr()
        {
            var builder = new PredicateTemplateBuilder<User>();
            var idIs0 = builder.Create(u => u.Id == 0);
            var nameIsAdmin = builder.Create(u => u.Name == "ADMIN");
            var nameIsRoot = builder.Create(u => u.Name == "ROOT");
            var createTimeIsNull = builder.Create(u => u.CreateTime == null);

            var pre = nameIsAdmin.Or(nameIsRoot);
            pre = pre.And(idIs0);
            pre = pre.And(createTimeIsNull);

            User user = new User() { Id = 1, Name = "fc", CreateTime = DateTime.Now };
            Assert.IsFalse(pre.IsTrue(user));
        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime? CreateTime { get; set; }
        }
    }
}
