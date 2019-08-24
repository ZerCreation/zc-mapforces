using ZerCreation.MapForcesEngine.Map.Cartographer;

namespace ZerCreation.MapForces.WebApi.Logic
{
    /// <summary>
    /// Use to keep GameController's logic
    /// </summary>
    public class EngineGateway
    {
        private readonly ICartographer cartographer;

        public EngineGateway(ICartographer cartographer)
        {
            this.cartographer = cartographer;
        }

        // TODO: Use to create map MoveOperation using Cartographer's knowledge
    }
}
