using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Operations;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine
{
    /// <summary>
    /// Entry point for game control from external front library
    /// </summary>
    public class GameController
    {
        private readonly MoveService moveService;
        private readonly ICartographer cartographer;

        public GameController(MoveService moveService, ICartographer cartographer)
        {
            this.moveService = moveService;
            // Cartographer must be singleton
            this.cartographer = cartographer;
        }

        public void BuildMap(int width, int height)
        {
            var mapSettings = new MapSettings
            {
                Width = width,
                Height = height
            };

            this.cartographer.DrawMap(mapSettings);
        }

        public void InitializePlayers()
        {

        }

        public void Move(IEnumerable<Tuple<int, int>> armyToMove, IEnumerable<Tuple<int, int>> moveTarget)
        {
            // TODO: Check if map and players are initialized

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
