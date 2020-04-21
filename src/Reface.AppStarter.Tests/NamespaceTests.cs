using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppModules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class NamespaceTests
    {
        [TestMethod]
        public void PrintNamespaceByNameOf()
        {
            Console.WriteLine(nameof(Reface.AppStarter.Tests));
        }

        [A(Prefix = new string[] { "a" })]
        [TestMethod]
        public void GetNamespace()
        {
            List<Type> types = new List<Type>()
            {
                typeof(NamespaceTests),
                typeof(IList),
                typeof(AppModule)
            };
            foreach (var dll in types.Select(x => x.Assembly))
            {
                foreach (var t in dll.GetTypes())
                    Console.WriteLine(t.FullName);
            }
        }

        public class AAttribute : Attribute
        {
            public string[] Prefix { get; set; }
        }
    }
}
