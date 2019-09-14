using ZerCreation.MapForces.WebApi.Dtos;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForces.WebApi.Mappers
{
    public static class PlayerMapper
    {
        public static PlayerDto Map(IPlayer player)
        {
            return new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                Color = player.Color
            };
        }
    }
}
