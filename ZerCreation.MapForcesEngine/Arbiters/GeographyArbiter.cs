using System;
using ZerCreation.MapForcesEngine.Move;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    public class GeographyArbiter : IArbiter
    {
        /// <summary>
        /// Takes into account terrain structure of the way of <paramref name="movingArmy"/>
        /// </summary>
        /// <param name="movingArmy">Army which is input and output as result</param>
        /// <param name="areaTarget">Area which is input and output as result</param>
        /// <returns>Returns false if there is no move possible</returns>
        public bool SolveMove(MoveOperation moveOperation)
        {
            // Now not used
            throw new NotImplementedException();

            //.Where(unit => unit.Type is Earth)
            //    .Where(unit => unit.Type is WaterCorridor)
        }
    }
}
