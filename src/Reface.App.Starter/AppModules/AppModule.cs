using System;
using System.Collections.Generic;

namespace Reface.AppStarter.AppModules
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AppModule : Attribute, IAppModule
    {
        private readonly List<IAppModule> dependentModules = new List<IAppModule>();

        public AppModule()
        {
            object[] attrs = this.GetType().GetCustomAttributes(true);
            foreach (var attr in attrs)
            {
                if (!(attr is AppModule)) continue;
                dependentModules.Add(attr as AppModule);
            }
        }

        public virtual IEnumerable<IAppModule> DependentModules => dependentModules;

        public virtual void OnUsing(AppSetup setup, IAppModule targetModule)
        {
        }
    }
}
