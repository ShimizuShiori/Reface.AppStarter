using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.ConfigRegistions;
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
            var info = new AttributeAndTypeInfo(new ConfigAttribute("S1"), typeof(TestConfig));
            List<IConfigRegistion> registions = new List<IConfigRegistion>();
            registions.Add(new ScannedConfigRegistion(info));
            string json = g.Generate(registions);
            Console.WriteLine(json);
        }
    }
}
