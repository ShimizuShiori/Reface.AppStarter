namespace Reface.AppStarter
{
    public class ApplicationEnvironmentBuiltEventArgs
    {
        public IComponentFactory ComponentFactory { get; private set; }

        public ApplicationEnvironmentBuiltEventArgs(IComponentFactory componentFactory)
        {
            ComponentFactory = componentFactory;
        }
    }
}
