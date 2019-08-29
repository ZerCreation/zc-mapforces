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

namespace ZerCreation.MapForces.WebApi.Logic
{
    /// <summary>
    /// Entry point for game control
    /// </summary>
    public class EngineDispatcher
    {
        private readonly MoveService moveService;
        private readonly ICartographer cartographer;
        private readonly MapBuilder mapCreator;

        public EngineDispatcher(MoveService moveService, ICartographer cartographer, MapBuilder mapCreator)
        {
            this.moveService = moveService;
            this.cartographer = cartographer;
            this.mapCreator = mapCreator;
        }

        public GamePlayDetailsDto BuildNewGamePlay()
        {
            MapDescription mapDescription = this.mapCreator.BuildFromFile();

            this.cartographer.SaveMapWorld(mapDescription);

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
            // TODO: Check if map and players are initialized
            var moveOperation = new MoveOperation
            {
                Player = this.cartographer.FindPlayerById(moveDto.PlayerId),
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

            return this.moveService.Move(moveOperation);
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
                .Select(player => new PlayerDto
                {
                    Id = player.Id,
                    Name = player.Name,
                    Color = "blue"
                })
                .Distinct();
        }
    }
}
