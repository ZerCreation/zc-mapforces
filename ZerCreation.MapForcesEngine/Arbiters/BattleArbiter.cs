﻿using System;
using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    public class BattleArbiter : IArbiter
    {
        /// <summary>
        /// Calculates battle results in <paramref name="movingArmy"/> way
        /// </summary>
        /// <param name="movingArmy">Army which is input and output as result</param>
        /// <param name="areaTarget">Area which is input and output as result</param>
        /// <returns>Returns false if there is no move possible</returns>
        public bool SolveMove(MovingArmy movingArmy, List<AreaUnit> areaTarget)
        {
            throw new NotImplementedException();
        }
    }
}
