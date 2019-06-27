using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZerCreation.MapForces.WebApi.Dtos;
using ZerCreation.MapForces.WebApi.HubConfig;
using ZerCreation.MapForces.WebApi.Mappers;
using ZerCreation.MapForcesEngine;
using ZerCreation.MapForcesEngine.Map;

namespace ZerCreation.MapForces.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly EngineDispatcher engineDispatcher;
        private readonly IHubContext<GameHub> gameHubContext;

        public GameController(
            EngineDispatcher engineDispatcher,
            IHubContext<GameHub> gameHubContext)
        {
            this.engineDispatcher = engineDispatcher;
            this.gameHubContext = gameHubContext;
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
        public async Task<ActionResult<GamePlayDetailsDto>> JoinToGame()
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
                        Y = unit.Position.Y,
                        Ownership = OwnershipMapper.MapToDto(unit.PlayerPossesion)
                    })
            };

            await this.gameHubContext.Clients.All.SendAsync("actionsnotification", "player joined");

            return this.Ok(gameDescription);
        }
    }
}