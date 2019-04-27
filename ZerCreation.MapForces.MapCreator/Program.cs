using System;

namespace ZerCreation.MapForces.MapCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            new Creator().Run();

            Console.WriteLine("Creation process was finished.");
            Console.ReadKey();
        }
    }
}
