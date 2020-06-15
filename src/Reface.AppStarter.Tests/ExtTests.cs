using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Tests.Events;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class ExtTests
    {
        [TestMethod]
        public void PublishEvent()
        {
            var app = new AppSetup().Start(new TestAppModule());
            using (var work = app.BeginWork("t1"))
            {
                work.PublishEvent(new TestEvent(this));
                Assert.AreEqual("TEST", app.Context["TEST"]);
            }
        }

        [TestMethod]
        [DataRow(new string[] { "a", "b", "c" }, ",", "a,b,c")]
        [DataRow(null, ",", "")]
        [DataRow(new string[] { }, ",", "")]
        [DataRow(new string[] { "a" }, ",", "a")]
        [DataRow(new string[] { "a", "b", "c" }, null, "abc")]
        public void JoinTest(string[] list, string joiner, string result)
        {
            Assert.AreEqual(result, list.Join(joiner));
        }

        [TestMethod]
        [DataRow("a_b_c_d", "_", new string[] { "a", "b", "c", "d" })]
        [DataRow("a123b123c123d", "123", new string[] { "a", "b", "c", "d" })]
        [DataRow("a123b123c123d123", "123", new string[] { "a", "b", "c", "d", "" })]
        [DataRow("", "123", new string[] { "" })]
        [DataRow(null, "123", new string[] { })]
        [DataRow("12345", null, new string[] { "1", "2", "3", "4", "5" })]
        [DataRow("12345", "", new string[] { "1", "2", "3", "4", "5" })]
        public void SplitTest(string text, string splitter, string[] result)
        {
            IEnumerable<string> tokens = text.Split(splitter);
            Assert.AreEqual(result.Length, tokens.Count());
            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(result[i], tokens.ElementAt(i));
            }
        }
    }
}
