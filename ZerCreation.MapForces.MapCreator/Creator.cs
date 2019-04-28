using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ZerCreation.MapForces.MapCreator.Parsers;
using ZerCreation.MapForcesEngine.Map;

namespace ZerCreation.MapForces.MapCreator
{
    public class Creator
    {
        private readonly IParser parser;
        private readonly IFormatter formatter;

        public Creator()
        {
            this.parser = new AmChartsParser();
            this.formatter = new BinaryFormatter();
        }

        internal void Run()
        {
            MapDescription map = Parse();

            this.Save(map);

            this.Verify(formatter);
        }

        private MapDescription Parse()
        {
            try
            {
                string mapText = File.ReadAllText(@"C:\Zer Creation\Projects\Map Forces\Maps\amCharts.pixelMap.html");
                MapDescription map = this.parser.ParseToMap(mapText);

                return map;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Save(MapDescription map)
        {
            using (Stream stream = new FileStream("map.mfm", FileMode.Create, FileAccess.Write))
            {
                this.formatter.Serialize(stream, map);
            }
        }

        private void Verify(IFormatter formatter)
        {
            using (var streamToValidate = new FileStream("map.mfm", FileMode.Open, FileAccess.Read))
            {
                var readMap = (MapDescription)this.formatter.Deserialize(streamToValidate);
            }
        }
    }
}
