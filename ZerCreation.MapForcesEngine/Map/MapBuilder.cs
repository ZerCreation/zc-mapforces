using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ZerCreation.MapForcesEngine.Map
{
    public class MapBuilder
    {
        public MapDescription BuildFromFile()
        {
            IFormatter formatter = new BinaryFormatter();

            string filePath = @"C:\Zer Creation\Projects\Map Forces\ZerCreation.MapForcesEngine\ZerCreation.MapForces.MapCreator\bin\Debug\netcoreapp2.1\map.mfm";
            using (FileStream streamToValidate = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var readMap = (MapDescription)formatter.Deserialize(streamToValidate);
                return readMap;
            }
        }
    }
}
