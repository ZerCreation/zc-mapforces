using System.Collections;
using System.Collections.Generic;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class Army
    {
        public List<MovingUnit> Units { get; set; }
        public Player PlayerPossesion { get; set; }

        internal MovingUnit FetchNextUnit()
        {
            IEnumerator enumerator = this.Units.GetEnumerator();
            enumerator.MoveNext();

            return (MovingUnit)enumerator.Current;
        }
    }
}
