using System;

namespace ZerCreation.MapForcesEngine.Play
{
    [Serializable()]
    public class Player : IPlayer
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Color { get; }
        public int MovePoints { get; set; }

        public Player(string name, string color)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Color = color;

            // Temporary
            this.MovePoints = int.MaxValue;
        }

    }
}
