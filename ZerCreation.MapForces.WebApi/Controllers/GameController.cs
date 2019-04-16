using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ZerCreation.MapForces.WebApi.Dtos;
using ZerCreation.MapForcesEngine;

namespace ZerCreation.MapForces.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly EngineDispatcher engineDispatcher;

        public GameController(EngineDispatcher engineDispatcher)
        {
            this.engineDispatcher = engineDispatcher;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Map Force game API.";
        }

        [HttpPost("init")]
        public ActionResult<GamePlayDetails> Initialize([FromBody] GameInitDto gameInitDto)
        {
            return this.Ok(gameInitDto.MapName);
        }

        [HttpPost("join")]
        public ActionResult<GamePlayDetails> JoinToGame()
        {
            // Find existing game
            // If not then create a new one
            // At the moment always create a new one
            this.engineDispatcher.BuildMap();

            var gameDescription = new GamePlayDetails
            {
                MapWidth = 100,
                MapHeight = 80,
                Units = new List<MapUnitDto>
                {
                    new MapUnitDto { TerrainType = TerrainTypeDto.Earth, X = 5, Y = 5 },
                    new MapUnitDto { TerrainType = TerrainTypeDto.Water, X = 15, Y = 5 },
                    new MapUnitDto { TerrainType = TerrainTypeDto.Earth, X = 25, Y = 6 },
                    new MapUnitDto { TerrainType = TerrainTypeDto.Earth, X = 35, Y = 6 },
                    new MapUnitDto { TerrainType = TerrainTypeDto.Earth, X = 45, Y = 7 },
                }
            };

            return this.Ok(gameDescription);
        }
    }
}