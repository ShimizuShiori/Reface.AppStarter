using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter
{
    public class Application : AppModule, IApplication
    {
        public ApplicationEnvironment Start(ApplicationEnvironmentSetup setup)
        {
            // 先使用本模块
            setup.Use(new StarterAppModel());
            setup.Use(this);
            return setup.Build();
        }
    }
}
