using System.Collections.Generic;

namespace ZerCreation.MapForces.WebApi.Dtos
{
    public class GamePlayDetails
    {
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public IEnumerable<MapUnitDto> Units { get; set; }
    }
}
