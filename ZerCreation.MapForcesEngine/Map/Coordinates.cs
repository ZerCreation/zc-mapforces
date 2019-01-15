namespace ZerCreation.MapForcesEngine.Map
{
    public struct Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static Vector operator -(Coordinates coordinates1, Coordinates coordinates2)
        {
            return new Vector
            {
                X = coordinates1.X - coordinates2.X,
                Y = coordinates1.Y - coordinates2.Y
            };
        }
    }
}
