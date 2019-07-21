using Autofac;
using ZerCreation.MapForces.WebApi.Logic;
using ZerCreation.MapForcesEngine.Map.Cartographer;

namespace ZerCreation.MapForces.WebApi.Configuration
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<EngineGateway>();
            builder.RegisterType<BasicCartographer>().As<ICartographer>();
        }
    }
}
