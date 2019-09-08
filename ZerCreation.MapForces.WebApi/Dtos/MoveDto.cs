using System;
using System.Collections.Generic;

namespace ZerCreation.MapForces.WebApi.Dtos
{
    public class MoveDto
    {
        public Guid PlayerId { get; set; }
        public IList<UnitDto> UnitsToMove { get; set; }
        public IList<UnitDto> UnitsTarget { get; set; }
    }
}