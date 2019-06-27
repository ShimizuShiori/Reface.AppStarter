using System;

// todo : remove [AutofacExt] from namespace
namespace Reface.AppStarter.AutofacExt
{
    public class ComponentCreatingEventArgs : EventArgs
    {
        public Type RequiredType { get; private set; }
        public object CreatedObject { get; private set; }
        public object ReplacedObject { get; private set; }
        public bool IsReplaced { get; private set; } = false;
        public IComponentManager ComponentManager { get; private set; }

        public ComponentCreatingEventArgs(IComponentManager componentManager, Type requiredType, object createdObject)
        {
            this.ComponentManager = componentManager;
            RequiredType = requiredType;
            CreatedObject = createdObject;
        }

        public void Replace(object newObject)
        {
            this.IsReplaced = true;
            this.ReplacedObject = newObject;
        }
    }
}