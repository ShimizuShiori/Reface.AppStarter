using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter
{
    public class ModuleComponentCreatingEventArgs : EventArgs
    {
        public Type RequiredType { get; private set; }
        public object CreatedObject { get; private set; }
        public object ReplacedObject { get; private set; }
        public bool IsReplaced { get; private set; } = false;
        public IComponentFactory ComponentFactory { get; private set; }

        public ModuleComponentCreatingEventArgs(IComponentFactory componentFactory, Type requiredType, object createdObject)
        {
            this.ComponentFactory = componentFactory;
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
