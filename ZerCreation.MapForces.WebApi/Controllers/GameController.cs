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
        public ActionResult<GameDescriptionDto> Initialize([FromBody] GameInitDto gameInitDto)
        {
            this.engineDispatcher.BuildMap(gameInitDto.MapName);

            return this.Ok(null);
        }

        [HttpPost("join")]
        public ActionResult<GameDescriptionDto> JoinToGame()
        {
            var gameDescription = new GameDescriptionDto
            {
                MapWidth = 100,
                MapHeight = 80,
                Terrain = new List<TerrainUnitDto>
                {
                    new TerrainUnitDto { Type = TerrainTypeDto.Earth, X = 5, Y = 5 },
                    new TerrainUnitDto { Type = TerrainTypeDto.Water, X = 15, Y = 5 },
                    new TerrainUnitDto { Type = TerrainTypeDto.Earth, X = 25, Y = 6 },
                    new TerrainUnitDto { Type = TerrainTypeDto.Earth, X = 35, Y = 6 },
                    new TerrainUnitDto { Type = TerrainTypeDto.Earth, X = 45, Y = 7 },
                }
            };

            return this.Ok(gameDescription);
        }
    }
}