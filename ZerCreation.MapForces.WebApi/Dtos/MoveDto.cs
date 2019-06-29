using System.Collections.Generic;

namespace ZerCreation.MapForces.WebApi.Dtos
{
    public class MoveDto
    {
        public IList<UnitDto> UnitsToMove { get; set; }
        public IList<UnitDto> UnitsTarget { get; set; }
    }
}