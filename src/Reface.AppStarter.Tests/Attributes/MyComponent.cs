using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MyComponent : ComponentAttribute
    {
        public MyComponent() : base(RegistionMode.AsSelf)
        {

        }
    }
}
