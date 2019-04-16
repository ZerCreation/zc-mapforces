using System.Collections.Generic;

namespace ZerCreation.MapForces.WebApi.Dtos
{
    public class GameDescriptionDto
    {
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public IEnumerable<TerrainUnitDto> Terrain { get; set; }
    }
}
