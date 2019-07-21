using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Enums;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Move
{
    public class MoveOperation
    {
        public Area TargetArea { get; set; }
        public Area SourceArea { get; set; }
        public MoveMode Mode { get; set; }
        public IPlayer Player { get; set; }
        public List<MoveUnitTracker> UnitsTrackers { get; set; }

        public MoveOperation()
        {
            this.UnitsTrackers = new List<MoveUnitTracker>();
        }

        public bool CheckIfMoveIsFinished()
        {
            return this.Player.MovePoints == 0
                || this.UnitsTrackers.All(tracker => tracker.IsMoveTargetReached);
        }

        internal void AddMovePath(Queue<Coordinates> movePath)
        {
            MoveUnitTracker unitTracker = new MoveUnitTracker(movePath);
            this.UnitsTrackers.Add(unitTracker);
        }
    }
}
