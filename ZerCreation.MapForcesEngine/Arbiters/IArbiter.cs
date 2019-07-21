using ZerCreation.MapForcesEngine.Move;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    interface IArbiter
    {
        bool SolveMove(MoveOperation moveOperation);
    }
}
