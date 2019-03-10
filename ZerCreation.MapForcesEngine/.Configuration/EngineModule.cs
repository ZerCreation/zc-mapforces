using Autofac;
using ZerCreation.MapForcesEngine.Controllers;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Configuration
{
    public class EngineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Cartographer>().As<ICartographer>();
            builder.RegisterType<MoveService>();
            builder.RegisterType<MoveController>();
            builder.RegisterType<TrackCreator>();
        }
    }
}
