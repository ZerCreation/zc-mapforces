using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForces.WebApi.Dtos;
using ZerCreation.MapForces.WebApi.Mappers;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Enums;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Map.Cartographer;
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
        private readonly ICartographer cartographer;
        private readonly MapBuilder mapCreator;
        private readonly TurnService turnService;

        public EngineGateway(
            MoveService moveService, 
            ICartographer cartographer, 
            MapBuilder mapCreator,
            TurnService turnService)
        {
            this.moveService = moveService;
            this.cartographer = cartographer;
            this.mapCreator = mapCreator;
            this.turnService = turnService;
        }

        public GamePlayDetailsDto BuildNewGamePlay()
        {
            MapDescription mapDescription = this.mapCreator.BuildFromFile();

            this.cartographer.SaveMapWorld(mapDescription);
            this.turnService.SetupPlayers(mapDescription);

            IEnumerable<PlayerDto> players = this.RetrieveAllPlayers(mapDescription);
            List<MapUnitDto> units = this.RetrieveAllUnits(mapDescription);

            return new GamePlayDetailsDto
            {
                MapWidth = mapDescription.Width,
                MapHeight = mapDescription.Height,
                Players = players,
                Units = units
            };
        }

        public IEnumerable<HashSet<AreaUnit>> Move(MoveDto moveDto)
        {
            this.turnService.ValidatePlayerCanMove(moveDto.PlayerId);

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

        public PlayerDto SwitchToNextTurn()
        {
            this.turnService.SwitchToNextTurn();

            IPlayer player = this.turnService.CurrentPlayer;
            PlayerDto playerDto = PlayerMapper.Map(player);

            return playerDto;
        }

        private List<MapUnitDto> RetrieveAllUnits(MapDescription mapDescription)
        {
            return mapDescription.AreaUnits.Select(unit => new MapUnitDto
            {
                TerrainType = TerrainTypeDto.Earth,
                X = unit.Position.X,
                Y = unit.Position.Y,
                Ownership = OwnershipMapper.MapToDto(unit.PlayerPossesion)
            })
            .ToList();
        }

        private IEnumerable<PlayerDto> RetrieveAllPlayers(MapDescription mapDescription)
        {
            return mapDescription.AreaUnits
                .Select(unit => unit.PlayerPossesion)
                .Where(_ => _ != null)
                .Select(player => PlayerMapper.Map(player))
                .Distinct();
        }
    }
}
