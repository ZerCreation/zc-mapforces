using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

            Tuple<decimal, decimal>[] transformedPoints = this.CalculateToIntegerBased(points);

            IEnumerable<Point> mapPoints = transformedPoints
                .Select(tPoint => new Point
                {
                    X = (int)tPoint.Item1,
                    Y = (int)tPoint.Item2
                });

            return new Map
            {
                Points = transformedPoints.Select(tPoint => 
                    new Point
                    {
                        X = (int)tPoint.Item1,
                        Y = (int)tPoint.Item2
                    }).ToList()
            };
        }

        private Tuple<decimal, decimal>[] CalculateToIntegerBased(List<Tuple<decimal, decimal>> amPoints)
        {
            Tuple<decimal, decimal>[] sortedPointsByY = amPoints.OrderBy(point => point.Item2).ToArray();
            decimal prevY = sortedPointsByY.First().Item2;
            int pointYValue = 0;

            for (int i = 0; i < sortedPointsByY.Length; i++)
            {
                decimal diff = sortedPointsByY[i].Item2 - prevY;
                if (diff > 0)
                {
                    pointYValue++;
                }

                prevY = sortedPointsByY[i].Item2;
                sortedPointsByY[i] = new Tuple<decimal, decimal>(sortedPointsByY[i].Item1, pointYValue);
            }


            Tuple<decimal, decimal>[] sortedPointsByX = sortedPointsByY.OrderBy(point => point.Item1).ToArray();
            decimal prevX = sortedPointsByX.First().Item1;
            int pointXValue = 0;

            for (int i = 0; i < sortedPointsByX.Length; i++)
            {
                decimal diff = sortedPointsByX[i].Item1 - prevX;
                if (diff > 0)
                {
                    pointXValue++;
                }

                prevX = sortedPointsByX[i].Item1;
                sortedPointsByX[i] = new Tuple<decimal, decimal>(pointXValue, sortedPointsByX[i].Item2);
            }

            return sortedPointsByX.OrderBy(point => point.Item2).ToArray();
        }
    }
}
