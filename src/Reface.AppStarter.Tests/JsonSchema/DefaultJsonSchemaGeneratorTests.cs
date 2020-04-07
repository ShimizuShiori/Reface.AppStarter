using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.JsonSchema;
using Reface.AppStarter.Tests.Configs;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter.Tests.JsonSchema
{
    [TestClass]
    public class DefaultJsonSchemaGeneratorTests
    {
        [TestMethod]
        public void Gen()
        {
            DefaultJsonSchemaGenerator g = new DefaultJsonSchemaGenerator();
            List<AttributeAndTypeInfo> infos = new List<AttributeAndTypeInfo>();
            infos.Add(new AttributeAndTypeInfo(new ConfigAttribute("S1"), typeof(TestConfig)));
            string json = g.Generate(infos);
            Console.WriteLine(json);
        }
    }
}
