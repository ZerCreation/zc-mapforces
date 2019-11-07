using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
            if (this.engineGateway.IsGamePlayStarted)
            {
                return StatusCode(StatusCodes.Status409Conflict, "There is no space for new player. Game has already started.");
            }

            this.engineGateway.InitializeGameIfNotDone();

            GamePlayDetailsDto gamePlayDetailsDto = this.engineGateway.GetGamePlayForNextPlayer();

            await this.gameHubContext.Clients.All
                .SendAsync("generalNotification", $"Player(Id = {gamePlayDetailsDto.NewPlayerId}) joined game.");

            if (this.engineGateway.CanStartGamePlay)
            {
                await this.gameHubContext.Clients.All.SendAsync("generalNotification", "Game play is starting.");
                await this.SwitchToNextTurn();
            }

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
        public async Task<IActionResult> SwitchToNextTurn()
        {
            // TODO: Check id of turn changing player

            PlayerDto nextPlayerTurn = this.engineGateway.SwitchToNextPlayer();
            await this.gameHubContext.Clients.All.SendAsync("nextPlayerTurnNotification", nextPlayerTurn);

            return this.Ok();
        }
    }
}