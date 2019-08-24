using ZerCreation.MapForces.WebApi.Dtos;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForces.WebApi.Mappers
{
    public static class OwnershipMapper
    {
        public static OwnershipDto MapToDto(IPlayer player)
        {
            if (player == null)
            {
                return null;
            }

            return new OwnershipDto
            {
                PlayerId = player.Id,
                Force = 1
            };
        }
    }
}
