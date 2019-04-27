using Autofac;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Configuration
{
    public class EngineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<BasicCartographer>().As<ICartographer>();
            builder.RegisterType<MoveService>();
            builder.RegisterType<EngineDispatcher>();
            builder.RegisterType<TrackCreator>();
            builder.RegisterType<MapBuilder>();
        }
    }
}
