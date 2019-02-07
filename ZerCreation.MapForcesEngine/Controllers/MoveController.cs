using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Operations;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Controllers
{
    public class MoveController
    {
        private readonly MoveService moveService;

        public MoveController(MoveService moveService)
        {
            this.moveService = moveService;
        }

        /// <summary>
        /// Entry point for moving from external UI library
        /// </summary>
        /// <param name="armyToMove"></param>
        /// <param name="moveTarget"></param>
        public void Move(IEnumerable<Tuple<int, int>> armyToMove, IEnumerable<Tuple<int, int>> moveTarget)
        {
            var moveOperation = new MoveOperation
            {
                Mode = MoveMode.Basic,
                MovingArmy = new Army
                {
                    Units = armyToMove.Select(unit => new MovingUnit(unit.Item1, unit.Item2)).ToList()
                },
                AreaTarget = new Area
                {
                    Units = moveTarget.Select(unit => new AreaUnit(unit.Item1, unit.Item2)).ToList()
                }
            };

            this.moveService.Move(moveOperation);
        }
    }
}
