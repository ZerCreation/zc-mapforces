using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Models;

namespace ZerCreation.MapForces.MapCreator.Parsers
{
    public class AmChartsParser : IParser
    {
        public MapDescription ParseToMap(string input)
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
            this.RevertHorizontally(transformedPoints);

            return new MapDescription
            {
                Width = (int)transformedPoints.Max(tPoint => tPoint.Item1),
                Height = (int)transformedPoints.Max(tPoint => tPoint.Item2),
                AreaUnits = transformedPoints
                    .OrderBy(tPoint => tPoint.Item1)
                    .ThenBy(tPoint => tPoint.Item2)
                    .Select(tPoint => new AreaUnit((int)tPoint.Item1, (int)tPoint.Item2))
                    .ToList()
            };
        }

        private Tuple<decimal, decimal>[] CalculateToIntegerBased(List<Tuple<decimal, decimal>> amPoints)
        {
            Tuple<decimal, decimal>[] sortedPointsByY = amPoints.OrderBy(point => point.Item2).ToArray();
            decimal prevY = sortedPointsByY.First().Item2;
            int pointYValue = 0;
            decimal minTypicalDiff = decimal.MaxValue;

            for (int i = 0; i < sortedPointsByY.Length; i++)
            {
                decimal diff = sortedPointsByY[i].Item2 - prevY;
                if (diff > 0)
                {
                    minTypicalDiff = Math.Min(diff, minTypicalDiff);
                    int diffScale = (int)(diff / minTypicalDiff);
                    pointYValue += diffScale;
                }

                prevY = sortedPointsByY[i].Item2;
                sortedPointsByY[i] = new Tuple<decimal, decimal>(sortedPointsByY[i].Item1, pointYValue);
            }


            Tuple<decimal, decimal>[] sortedPointsByX = sortedPointsByY.OrderBy(point => point.Item1).ToArray();
            decimal prevX = sortedPointsByX.First().Item1;
            int pointXValue = 0;
            minTypicalDiff = decimal.MaxValue;

            for (int i = 0; i < sortedPointsByX.Length; i++)
            {
                decimal diff = sortedPointsByX[i].Item1 - prevX;
                if (diff > 0)
                {
                    minTypicalDiff = Math.Min(diff, minTypicalDiff);
                    int diffScale = (int)(diff / minTypicalDiff);
                    pointXValue += diffScale;
                }

                prevX = sortedPointsByX[i].Item1;
                sortedPointsByX[i] = new Tuple<decimal, decimal>(pointXValue, sortedPointsByX[i].Item2);
            }

            return sortedPointsByX.OrderBy(point => point.Item2).ToArray();
        }

        private void RevertHorizontally(Tuple<decimal, decimal>[] transformedPoints)
        {
            decimal maxVertPoint = transformedPoints.Max(tPoint => tPoint.Item2);

            for (int i = 0; i < transformedPoints.Length; i++)
            {
                var newVertValue = Math.Abs(transformedPoints[i].Item2 - maxVertPoint);
                transformedPoints[i] = new Tuple<decimal, decimal>(transformedPoints[i].Item1, newVertValue);
            }
        }
    }
}
