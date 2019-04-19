using ZerCreation.MapForces.MapCreator.Models;

namespace ZerCreation.MapForces.MapCreator.Parsers
{
    interface IParser
    {
        Map ParseToMap(string input);
    }
}
