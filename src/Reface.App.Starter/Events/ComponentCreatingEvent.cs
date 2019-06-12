using Reface.AppStarter.AutofacExt;
using Reface.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.Events
{
    public class ComponentCreatingEvent : Event
    {
        private ComponentCreatingEventArgs e;

        public Type RequiredType
        {
            get
            {
                return e.RequiredType;
            }
        }
        public object CreatedComponent
        {
            get { return e.CreatedObject; }
        }

        public IComponentManager ComponentManager
        {
            get { return e.ComponentManager; }
        }

        public void Replace(object newComponent)
        {
            e.Replace(newComponent);
        }

        public ComponentCreatingEvent(object source, ComponentCreatingEventArgs e) : base(source)
        {
            this.e = e;
        }
    }
}
