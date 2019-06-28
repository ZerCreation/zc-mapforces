using ZerCreation.MapForcesEngine.Models;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    public class DiplomacyArbiter : IArbiter
    {
        /// <summary>
        /// Checks if area can be conquered from diplomacy perspective
        /// </summary>
        /// <param name="movingArmy">Army which is input and output as result</param>
        /// <param name="areaTarget">Area which is input and output as result</param>
        /// <returns>Returns false if there is no move possible</returns>
        public bool SolveMove(MoveOperation moveOperation)
        {
            throw new System.NotImplementedException();
        }
    }
}
