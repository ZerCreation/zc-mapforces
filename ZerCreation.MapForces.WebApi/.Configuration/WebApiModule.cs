using Autofac;
using ZerCreation.MapForces.WebApi.Logic;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Move;
using ZerCreation.MapForcesEngine.Turns;

namespace ZerCreation.MapForces.WebApi.Configuration
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            // Use GamesDispatcher to resolve Cartographer and TurnService for current game in future
            builder.RegisterType<BasicCartographer>().As<ICartographer>()
                .SingleInstance();
            builder.RegisterType<TurnService>()
                .SingleInstance();

            builder.RegisterType<MoveService>();
            builder.RegisterType<EngineGateway>();
            builder.RegisterType<MapBuilder>();
        }
    }
}
