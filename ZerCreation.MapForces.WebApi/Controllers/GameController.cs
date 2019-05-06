using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForces.WebApi.Dtos;
using ZerCreation.MapForcesEngine;
using ZerCreation.MapForcesEngine.Map;

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
        public ActionResult<GamePlayDetailsDto> Initialize([FromBody] GameInitDto gameInitDto)
        {
            return this.Ok(gameInitDto.MapName);
        }

        [HttpPost("join")]
        public ActionResult<GamePlayDetailsDto> JoinToGame()
        {
            // Find existing game
            // If not then create a new one
            // At the moment always create a new one
            MapDescription mapDescription = this.engineDispatcher.BuildMap();

            var gameDescription = new GamePlayDetailsDto
            {
                MapWidth = mapDescription.Width,
                MapHeight = mapDescription.Height,
                Units = mapDescription.AreaUnits.Select(unit => 
                    new MapUnitDto
                    {
                        TerrainType = TerrainTypeDto.Earth,
                        X = unit.Position.X,
                        Y = unit.Position.Y
                    })
            };

            return this.Ok(gameDescription);
        }
    }
}