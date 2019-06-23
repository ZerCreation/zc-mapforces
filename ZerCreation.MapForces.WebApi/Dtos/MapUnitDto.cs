using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZerCreation.MapForces.WebApi.Dtos
{
    public class MapUnitDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public OwnershipDto Ownership { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TerrainTypeDto TerrainType { get; set; }
    }
}
