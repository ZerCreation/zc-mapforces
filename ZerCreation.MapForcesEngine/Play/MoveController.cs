using ZerCreation.MapForcesEngine.Arbiters;
using ZerCreation.MapForcesEngine.Operations;

namespace ZerCreation.MapForcesEngine.Play
{
    public class MoveController
    {
        private readonly ArbiterDispatcher arbiterDispatcher;

        public MoveController(ArbiterDispatcher arbiterDispatcher)
        {
            this.arbiterDispatcher = arbiterDispatcher;
        }

        public void Move(MoveOperation moveOperation)
        {
            this.arbiterDispatcher.SolveMove(moveOperation);
        }
}
}
