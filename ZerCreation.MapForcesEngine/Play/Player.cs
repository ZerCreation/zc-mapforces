using System;

namespace ZerCreation.MapForcesEngine.Play
{
    [Serializable()]
    public class Player
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public int MovePoints { get; set; }

        public Player(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

    }
}
