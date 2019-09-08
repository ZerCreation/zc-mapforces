using System;

namespace ZerCreation.MapForcesEngine.Play
{
    public interface IPlayer
    {
        Guid Id { get; }
        string Name { get; }
        string Color { get; }
        int MovePoints { get; set; }
    }
}