using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZerCreation.MapForces.WebApi.Dtos;
using ZerCreation.MapForces.WebApi.HubConfig;
using ZerCreation.MapForces.WebApi.Logic;
using ZerCreation.MapForces.WebApi.Mappers;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Turns;

namespace ZerCreation.MapForces.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly EngineGateway engineGateway;
        private readonly IHubContext<GameHub> gameHubContext;

        public GameController(
            EngineGateway engineGateway,
            IHubContext<GameHub> gameHubContext)
        {
            this.engineGateway = engineGateway;
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
            GamePlayDetailsDto gamePlayDetailsDto = this.engineGateway.BuildNewGamePlay();

            await this.gameHubContext.Clients.All.SendAsync("actionsnotification", "player joined");

            return this.Ok(gamePlayDetailsDto);
        }

        [HttpPost("move")]
        public async Task<IActionResult> Move(MoveDto moveDto)
        {
            IEnumerable<HashSet<AreaUnit>> unitsPathsResults;

            try
            {
                unitsPathsResults = this.engineGateway.Move(moveDto);
            }
            catch (WrongPlayerTurnException ex)
            {
                return this.BadRequest(ex.Message);
            }

            foreach (HashSet<AreaUnit> units in unitsPathsResults)
            {
                var unit = units.First();
                var mapUnitChanged = new MapUnitDto
                {
                    TerrainType = TerrainTypeDto.Earth,
                    X = unit.Position.X,
                    Y = unit.Position.Y,
                    Ownership = OwnershipMapper.MapToDto(unit.PlayerPossesion)
                };

                await this.gameHubContext.Clients.All.SendAsync("positionChangedNotification", mapUnitChanged);
            }

            return this.Ok();
        }

        [HttpPut("turn")]
        public async Task<ActionResult<PlayerDto>> SwitchToNextTurn()
        {
            PlayerDto nextPlayerTurn = this.engineGateway.SwitchToNextTurn();
            await this.gameHubContext.Clients.All.SendAsync("nextPlayerTurnNotification", nextPlayerTurn);

            return this.Ok();
        }
    }
}