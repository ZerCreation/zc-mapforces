using System.Collections;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class Army
    {
        private int unitsIterator = 0;

        public MovingUnit[][] Units { get; set; }
        public Player PlayerPossesion { get; set; }

        internal MovingUnit FetchNextUnit()
        {
            IEnumerator enumerator = this.Units.GetEnumerator();
            enumerator.MoveNext();

            return (MovingUnit)enumerator.Current;
        }
    }
}
