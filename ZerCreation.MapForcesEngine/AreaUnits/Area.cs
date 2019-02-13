using System.Collections;
using System.Collections.Generic;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class Area
    {
        public List<AreaUnit> Units { get; set; }

        internal AreaUnit FetchNextUnit()
        {
            IEnumerator enumerator = this.Units.GetEnumerator();
            enumerator.MoveNext();

            return (AreaUnit)enumerator.Current;
        }
    }
}
