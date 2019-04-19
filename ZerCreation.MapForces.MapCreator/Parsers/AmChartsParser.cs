using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ZerCreation.MapForces.MapCreator.Models;

namespace ZerCreation.MapForces.MapCreator.Parsers
{
    public class AmChartsParser : IParser
    {
        public Map ParseToMap(string input)
        {
            var jsonRegex = new Regex(@"\""images\"": (?<jsonArray>(\[[^\]]*)])");
            Match match = jsonRegex.Match(input);
            string jsonText = match.Groups["jsonArray"].Value;

            var points = new List<Tuple<decimal, decimal>>();
            dynamic json = JsonConvert.DeserializeObject(jsonText);
            foreach (var item in json)
            {
                if (item["pixelMapperLogo"] != null)
                {
                    continue;
                }

                decimal longitude = decimal.Parse(Convert.ToString(item["longitude"]));
                decimal latitude = decimal.Parse(Convert.ToString(item["latitude"]));
                points.Add(new Tuple<decimal, decimal>(longitude, latitude));
            }

            return new Map
            {
                Points = new List<Point>
                {
                    new Point { X = 5, Y = 10 },
                    new Point { X = 40, Y = 90 }
                }
            };
        }
    }
}
