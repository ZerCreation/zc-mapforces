using ZerCreation.MapForces.WebApi.Dtos;
using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForces.WebApi.Mappers
{
    public static class MapUnitMapper
    {
        public static MapUnitDto Map(AreaUnit areaUnit)
        {
            return new MapUnitDto
            {
                TerrainType = TerrainTypeDto.Earth,
                X = areaUnit.Position.X,
                Y = areaUnit.Position.Y,
                Ownership = OwnershipMapper.MapToDto(areaUnit.PlayerPossesion)
            };
        }
    }
}
