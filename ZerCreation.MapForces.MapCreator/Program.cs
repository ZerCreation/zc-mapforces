using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ZerCreation.MapForces.MapCreator.Models;

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
