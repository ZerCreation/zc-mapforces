using System.Collections;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class Area
    {
        public AreaUnit[][] Units { get; set; }

        internal AreaUnit FetchNextUnit()
        {
            IEnumerator enumerator = this.Units.GetEnumerator();
            enumerator.MoveNext();

            return (AreaUnit)enumerator.Current;
        }
    }
}
