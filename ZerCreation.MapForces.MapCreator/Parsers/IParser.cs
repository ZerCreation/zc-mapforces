using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Models;

namespace ZerCreation.MapForces.MapCreator.Parsers
{
    interface IParser
    {
        MapDescription ParseToMap(string input);
    }
}
