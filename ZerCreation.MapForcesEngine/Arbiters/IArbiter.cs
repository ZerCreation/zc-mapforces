using ZerCreation.MapForcesEngine.Operations;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    interface IArbiter
    {
        bool SolveMove(MoveOperation moveOperation);
    }
}
