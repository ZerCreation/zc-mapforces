using ZerCreation.MapForcesEngine.Map;

namespace ZerCreation.MapForces.MapCreator.Parsers
{
    interface IParser
    {
        MapDescription ParseToMap(string input);
    }
}
