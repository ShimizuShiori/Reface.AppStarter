using ClassLibrary2;
using Reface.AppStarter;
using Reface.AppStarter.AppModules;
using System;

namespace ClassLibrary1
{
    [ComponentScanAppModule]
    [Cb2AppModule]
    public class CL1AppModule : AppModule
    {
        public override void OnUsing(AppModuleUsingArguments args)
        {
            foreach (var info in args.ScannedAttributeAndTypeInfos)
            {
                Console.WriteLine($"{this.GetType()} : {info.Type}");
            }
        }
    }
}
