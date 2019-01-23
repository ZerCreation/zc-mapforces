using ZerCreation.MapForcesEngine.Arbiters;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Operations;

namespace ZerCreation.MapForcesEngine.Play
{
    public class MoveService
    {
        private readonly ArbiterDispatcher arbiterDispatcher;
        private readonly Cartographer cartographer;

        public MoveService(ArbiterDispatcher arbiterDispatcher, Cartographer cartographer)
        {
            this.arbiterDispatcher = arbiterDispatcher;
            this.cartographer = cartographer;
        }

        public void Move(MoveOperation moveOperation)
        {
            // TODO: Check cartographer to assign Army.PlayerPosession
            // TODO: Check cartographer to assign Army.Units.Value

            this.arbiterDispatcher.SolveMove(moveOperation);

            this.cartographer.UpdateUnitPossesion(
                moveOperation.AreaTarget.Units, 
                moveOperation.MovingArmy.PlayerPossesion);
        }
}
}
