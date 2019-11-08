using Autofac;
using ZerCreation.MapForcesEngine.Map;

namespace ZerCreation.MapForcesEngine.Configuration
{
    public class EngineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<TrackCreator>();
        }
    }
}
