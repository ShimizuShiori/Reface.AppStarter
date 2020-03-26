//using Reface.AppStarter.AppContainers;
//using System;

//namespace Reface.AppStarter.Tests.AppContainers
//{
//    public class TestContainer : IAppContainer
//    {
//        public void Dispose()
//        {
//        }

//        public void OnAppPrepair(App app)
//        {
//            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
//            componentContainer.ComponentCreating += ComponentContainer_ComponentCreating;
//        }

//        public void OnAppStarted(App app)
//        {
//        }

//        private void ComponentContainer_ComponentCreating(object sender, AutofacExt.ComponentCreatingEventArgs e)
//        {
//            Console.WriteLine("TestContainer : {0} -> {1}", e.RequiredType, e.CreatedObject.GetType());
//        }
//    }
//}
