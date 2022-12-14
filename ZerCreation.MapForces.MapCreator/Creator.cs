using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ZerCreation.MapForces.MapCreator.Parsers;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Models;
using ZerCreation.MapForcesEngine.Play;

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
            MapDescription map = this.Parse();

            this.Save(map);

            this.Verify();
        }

        private MapDescription Parse()
        {
            try
            {
                string mapText = File.ReadAllText(@"C:\Zer Creation\Projects\Map Forces\Maps\amCharts.pixelMap.World-Mercator.html");
                MapDescription map = this.parser.ParseToMap(mapText);
                this.DefinePlayersStartUnits(map.AreaUnits);

                return map;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void DefinePlayersStartUnits(List<AreaUnit> areaUnits)
        {
            var random = new Random();

            var coloredPlayers = new Dictionary<string, string>
            {
                { "ZwRst", "blue" }, { "Lemnzer", "green" },
                //{ "Renox", "red" }, { "Zenirun", "cyan" }, { "ShadowZ", "orange" }
            };

            foreach (var cPlayer in coloredPlayers)
            {
                AreaUnit playerUnit = areaUnits[random.Next(areaUnits.Count)];
                playerUnit.PlayerPossesion = new Player(cPlayer.Key, cPlayer.Value);
            }
        }

        private void Save(MapDescription map)
        {
            using (Stream stream = new FileStream("map.mfm", FileMode.Create, FileAccess.Write))
            {
                this.formatter.Serialize(stream, map);
            }
        }

        private void Verify()
        {
            using (var streamToValidate = new FileStream("map.mfm", FileMode.Open, FileAccess.Read))
            {
                var readMap = (MapDescription)this.formatter.Deserialize(streamToValidate);
            }
        }
    }
}
