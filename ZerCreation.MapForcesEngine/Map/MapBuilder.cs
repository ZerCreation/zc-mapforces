using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ZerCreation.MapForcesEngine.Models;

namespace ZerCreation.MapForcesEngine.Map
{
    public class MapBuilder
    {
        public MapDescription BuildFromFile()
        {
            string filePath = @"C:\Zer Creation\Projects\Map Forces\ZerCreation.MapForcesEngine\ZerCreation.MapForces.MapCreator\bin\Debug\netcoreapp2.1\map.mfm";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IFormatter formatter = new BinaryFormatter();
                var readMap = (MapDescription)formatter.Deserialize(fileStream);
                return readMap;
            }
        }
    }
}
