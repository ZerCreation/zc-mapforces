using System;

namespace ZerCreation.MapForces.WebApi.Dtos
{
    public class OwnershipDto
    {
        public Guid PlayerId { get; set; }
        public int Force { get; set; }
    }
}