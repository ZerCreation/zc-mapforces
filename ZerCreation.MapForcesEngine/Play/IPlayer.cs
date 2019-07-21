using System;

namespace ZerCreation.MapForcesEngine.Play
{
    public interface IPlayer
    {
        Guid Id { get; }
        int MovePoints { get; set; }
        string Name { get; set; }
    }
}