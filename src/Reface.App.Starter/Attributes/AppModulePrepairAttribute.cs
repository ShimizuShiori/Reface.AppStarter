using System;

namespace Reface.AppStarter.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class AppModulePrepairAttribute : Attribute
    {
        public abstract void Prepair(AppModulePrepareArguments args);
    }
}
