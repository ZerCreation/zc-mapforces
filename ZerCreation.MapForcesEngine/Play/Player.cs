using System;

namespace ZerCreation.MapForcesEngine.Play
{
    [Serializable()]
    public class Player
    {
        public Player(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
        public int MovePoints { get; set; }
    }
}
