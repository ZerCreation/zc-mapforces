using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Enums;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Models;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine
{
    /// <summary>
    /// Entry point for game control from external front library
    /// </summary>
    public class EngineDispatcher
    {
        private readonly MoveService moveService;
        private readonly ICartographer cartographer;
        private readonly MapBuilder mapCreator;

        public EngineDispatcher(MoveService moveService, ICartographer cartographer, MapBuilder mapCreator)
        {
            this.moveService = moveService;
            // Cartographer must be singleton
            this.cartographer = cartographer;
            this.mapCreator = mapCreator;
        }

        public MapDescription BuildMap()
        {
            MapDescription builtMap = this.mapCreator.BuildFromFile();

            this.cartographer.DrawMap(builtMap);

            return builtMap;
        }

        public void InitializePlayers()
        {

        }

        public void Move(MoveOperation moveOperation)
        {
            // TODO: Check if map and players are initialized

            this.moveService.Move(moveOperation);
        }
    }
}
