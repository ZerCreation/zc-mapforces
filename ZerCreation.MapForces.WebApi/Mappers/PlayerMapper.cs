using ZerCreation.MapForces.WebApi.Dtos;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForces.WebApi.Mappers
{
    public static class PlayerMapper
    {
        public static PlayerDto MapToDto(Player player)
        {
            if (player == null)
            {
                return null;
            }

            return new PlayerDto
            {
                Name = player.Name
            };
        }
    }
}
