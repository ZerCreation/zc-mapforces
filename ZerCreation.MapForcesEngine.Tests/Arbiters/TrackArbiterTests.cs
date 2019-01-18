using NUnit.Framework;
using ZerCreation.MapForcesEngine.Arbiters;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Operations;

namespace ZerCreation.MapForcesEngine.Tests.Arbiters
{
    [TestFixture]
    public class TrackArbiterTests
    {
        private TrackArbiter sut;

        [SetUp]
        public void Init()
        {
            this.sut = new TrackArbiter();
        }

        [Test]
        public void Should_move_one_unit_basically_RightDown()
        {
            // Given
            var movingUnit = new MovingUnit
            {
                Position = new Coordinates
                {
                    X = 10,
                    Y = 10
                }
            };
            MovingUnit[][] movingUnits = new MovingUnit[1][];
            movingUnits[0][0] = movingUnit;

            var areaUnit = new AreaUnit
            {
                Position = new Coordinates
                {
                    X = 20,
                    Y = 20
                }
            };
            AreaUnit[][] areaUnits = new AreaUnit[1][];
            areaUnits[0][0] = areaUnit;

            var moveOperation = new MoveOperation
            {
                Mode = MoveMode.Basic,
                MovingArmy = new Army()
                {
                    Units = movingUnits
                },
                AreaTarget = new Area
                {
                    Units = areaUnits
                }
            };

            // When
            this.sut.SolveMove(moveOperation);

            // Then
            
        }
    }
}
