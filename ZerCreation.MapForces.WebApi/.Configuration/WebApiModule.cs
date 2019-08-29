using Autofac;
using ZerCreation.MapForces.WebApi.Logic;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Move;

namespace ZerCreation.MapForces.WebApi.Configuration
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder.RegisterType<BasicCartographer>().As<ICartographer>()
                .SingleInstance();

            builder.RegisterType<MoveService>();
            builder.RegisterType<EngineDispatcher>();
            builder.RegisterType<MapBuilder>();
        }
    }
}
