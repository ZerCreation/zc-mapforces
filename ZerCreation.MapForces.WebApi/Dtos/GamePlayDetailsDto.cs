using System;
using System.Collections.Generic;

namespace ZerCreation.MapForces.WebApi.Dtos
{
    public class GamePlayDetailsDto
    {
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public IList<MapUnitDto> Units { get; set; }
        public IEnumerable<PlayerDto> Players { get; set; }
        public Guid CurrentPlayerId { get; set; }
    }
}
