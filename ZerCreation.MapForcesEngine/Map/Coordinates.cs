using System;

namespace ZerCreation.MapForcesEngine.Map
{
    [Serializable()]
    public struct Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static bool operator ==(Coordinates coordinates1, Coordinates coordinates2)
        {
            return (coordinates1.X == coordinates2.X) && (coordinates1.Y == coordinates2.Y);
        }

        public static bool operator !=(Coordinates coordinates1, Coordinates coordinates2)
        {
            return (coordinates1.X != coordinates2.X) || (coordinates1.Y != coordinates2.Y);
        }

        public static Vector operator -(Coordinates coordinates1, Coordinates coordinates2)
        {
            return new Vector
            {
                X = coordinates1.X - coordinates2.X,
                Y = coordinates1.Y - coordinates2.Y
            };
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Coordinates))
            {
                return false;
            }

            var coordinates = (Coordinates)obj;
            return (this.X == coordinates.X) && (this.Y == coordinates.Y);
        }

        public override string ToString()
        {
            return $"X: {this.X}, Y: {this.Y}";
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}
