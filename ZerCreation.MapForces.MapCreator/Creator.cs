using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ZerCreation.MapForces.MapCreator.Models;
using ZerCreation.MapForces.MapCreator.Parsers;

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
            Map map = Parse();

            this.Save(map);

            this.Verify(formatter);
        }

        private Map Parse()
        {
            try
            {
                string mapText = File.ReadAllText(@"C:\Zer Creation\Projects\Map Forces\Maps\amCharts.pixelMapLight.html");
                Map map = this.parser.ParseToMap(mapText);

                return map;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Save(Map map)
        {
            using (Stream stream = new FileStream("map.mfm", FileMode.Create, FileAccess.Write))
            {
                this.formatter.Serialize(stream, map);
            }
        }

        private void Verify(IFormatter formatter)
        {
            var streamToValidate = new FileStream("map.mfm", FileMode.Open, FileAccess.Read);
            var readMap = (Map)this.formatter.Deserialize(streamToValidate);
        }
    }
}
