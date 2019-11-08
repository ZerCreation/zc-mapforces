using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForces.WebApi.Dtos;
using ZerCreation.MapForces.WebApi.Mappers;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Enums;
using ZerCreation.MapForcesEngine.Gameplay;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Models;
using ZerCreation.MapForcesEngine.Move;
using ZerCreation.MapForcesEngine.Play;
using ZerCreation.MapForcesEngine.Turns;

namespace ZerCreation.MapForces.WebApi.Logic
{
    /// <summary>
    /// Entry point for game control
    /// </summary>
    public class EngineGateway
    {
        private readonly MoveService moveService;
        private readonly MapBuilder mapCreator;
        private readonly TurnService turnService;
        private readonly GameplayInitializer gameplayInitializer;

        public bool CanStartGamePlay => this.gameplayInitializer.HaveAllPlayersJoined;
        public bool IsGamePlayStarted => this.turnService.CurrentPlayer != null;
        public int RoundId => this.turnService.RoundId;

        public EngineGateway(
            MoveService moveService, 
            MapBuilder mapCreator,
            TurnService turnService,
            GameplayInitializer gameplayInitializer)
        {
            this.moveService = moveService;
            this.mapCreator = mapCreator;
            this.turnService = turnService;
            this.gameplayInitializer = gameplayInitializer;
        }

        public void InitializeGameIfNotDone()
        {
            if (this.gameplayInitializer.IsMapInitialized)
            {
                return;
            }
            
            MapDescription mapDescription = this.mapCreator.BuildFromFile();
            this.gameplayInitializer.InitializeMap(mapDescription);
        }

        public GamePlayDetailsDto GetGamePlayForNextPlayer()
        {
            IEnumerable<PlayerDto> players = this.gameplayInitializer.Players
                .Select(player => PlayerMapper.Map(player));

            List<MapUnitDto> units = this.gameplayInitializer.AreaUnits
                .Select(areaUnit => MapUnitMapper.Map(areaUnit))
                .ToList();

            IPlayer newPlayer = this.gameplayInitializer.GetNextPlayerToInitialize();

            return new GamePlayDetailsDto
            {
                MapWidth = this.gameplayInitializer.MapWidth,
                MapHeight = this.gameplayInitializer.MapHeight,
                Players = players,
                Units = units,
                NewPlayerId = newPlayer.Id
            };
        }

        public IEnumerable<HashSet<AreaUnit>> Move(MoveDto moveDto)
        {
            this.turnService.ValidateItsPlayerTurn(moveDto.PlayerId);

            // TODO: Check if map and players are initialized
            var moveOperation = new MoveOperation
            {
                Player = this.turnService.CurrentPlayer,
                Mode = MoveMode.PathOfConquer,
                SourceArea = new Area
                {
                    Units = moveDto.UnitsToMove.Select(unit => new AreaUnit(unit.X, unit.Y)).ToList()
                },
                TargetArea = new Area
                {
                    Units = moveDto.UnitsTarget.Select(unit => new AreaUnit(unit.X, unit.Y)).ToList()
                }
            };

            // TODO: Map to and return MapUnitDto instead
            return this.moveService.Move(moveOperation);
        }

        public PlayerDto SwitchToNextPlayer()
        {
            this.turnService.SwitchToNextPlayer();

            IPlayer player = this.turnService.CurrentPlayer;
            PlayerDto playerDto = PlayerMapper.Map(player);

            return playerDto;
        }
    }
}
