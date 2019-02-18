using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Operations;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Tests.Play
{
    [TestFixture]
    public class MoveServiceTests
    {
        private MoveService sut;

        [SetUp]
        public void Init()
        {
            this.sut = new MoveService(new TrackCreator(), new Cartographer());
        }

        [TestCase(20, 20, 20, 10)]
        [TestCase(20, 20, 30, 10)]
        [TestCase(20, 20, 30, 20)]
        [TestCase(20, 20, 30, 30)]
        [TestCase(20, 20, 20, 30)]
        [TestCase(20, 20, 10, 30)]
        [TestCase(20, 20, 10, 20)]
        [TestCase(20, 20, 10, 10)]
        public void Single_unit_should_reach_target_around_itself(int armyX, int armyY, int areaX, int areaY)
        {
            // Given
            Army movingArmy = this.Given_CreateMovingArmy(new[] { new Coordinates(armyX, armyY) });
            Area areaTarget = this.Given_CreateTargetArea(new[] { new Coordinates(areaX, areaY) });

            var moveOperation = new MoveOperation
            {
                Mode = MoveMode.Basic,
                MovingArmy = movingArmy,
                AreaTarget = areaTarget
            };

            // When
            this.sut.Move(moveOperation);

            // Then
            Assert.AreEqual(
                areaTarget.Units.Single().Position,
                movingArmy.Units.Single().Position);
        }

        [Test]
        public void Single_unit_should_reach_five_moving_steps()
        {
            // Given
            Army movingArmy = this.Given_CreateMovingArmy(new[] { new Coordinates(0, 0) }, playerMovePoints: 1000);

            var pathSteps = new List<Area>
            {
                this.Given_CreateTargetArea(new[] { new Coordinates(10, 20) }),
                this.Given_CreateTargetArea(new[] { new Coordinates(5, 30) }),
                this.Given_CreateTargetArea(new[] { new Coordinates(40, 30) }),
                this.Given_CreateTargetArea(new[] { new Coordinates(80, 25) }),
                this.Given_CreateTargetArea(new[] { new Coordinates(100, 50) })
            };

            foreach (Area step in pathSteps)
            {
                var moveOperation = new MoveOperation
                {
                    Mode = MoveMode.Basic,
                    MovingArmy = movingArmy,
                    AreaTarget = step
                };

                // When
                this.sut.Move(moveOperation);
            }

            // Then
            AreaUnit finalAreaTarget = pathSteps.Last().Units.Single();
            Assert.AreEqual(
                finalAreaTarget.Position,
                movingArmy.Units.Single().Position);
        }

        private Area Given_CreateTargetArea(IEnumerable<Coordinates> initPositions)
        {
            List<AreaUnit> units = initPositions
                .Select(position => new AreaUnit(position))
                .ToList();

            return new Area
            {
                Units = units
            };
        }

        private Army Given_CreateMovingArmy(IEnumerable<Coordinates> initPositions, int playerMovePoints = 100)
        {
            Player player = Substitute.For<Player>();
            player.MovePoints = playerMovePoints;

            List<MovingUnit> units = initPositions
                .Select(position => new MovingUnit(position))
                .ToList();

            Army movingArmy = new Army
            {
                PlayerPossesion = player,
                Units = units
            };

            return movingArmy;
        }
    }
}
