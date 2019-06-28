using ZerCreation.MapForcesEngine.Models;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    interface IArbiter
    {
        bool SolveMove(MoveOperation moveOperation);
    }
}
