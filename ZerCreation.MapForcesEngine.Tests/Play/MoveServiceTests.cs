using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Enums;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Move;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Tests.Play
{
    [TestFixture]
    public class MoveServiceTests
    {
        private IFixture fixture;
        private IPlayer player;
        private MoveService sut;

        [SetUp]
        public void Init()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            var cartographer = this.fixture.Create<ICartographer>();
            cartographer.FindAreaUnit(Arg.Any<Coordinates>()).Returns(new AreaUnit(0, 0));

            this.player = this.fixture.Create<IPlayer>();
            this.player.MovePoints = 1000;

            this.sut = new MoveService(new TrackCreator(), cartographer);
        }

        [TestCase(20, 20, 20, 10)]
        [TestCase(20, 20, 30, 10)]
        [TestCase(20, 20, 30, 20)]
        [TestCase(20, 20, 30, 30)]
        [TestCase(20, 20, 20, 30)]
        [TestCase(20, 20, 10, 30)]
        [TestCase(20, 20, 10, 20)]
        [TestCase(20, 20, 10, 10)]
        public void Circle_army_should_reach_target_around_itself(int armyX, int armyY, int areaX, int areaY)
        {
            // Given
            int circleRadius = 3; //new Random().Next(1, 5);
            Area movingArmy = this.Given_CreateCircleArmy(new Coordinates(armyX, armyY), circleRadius);
            Area areaTarget = this.Given_CreateCircleArea(new Coordinates(areaX, areaY), circleRadius);

            var moveOperation = new MoveOperation
            {
                Player = this.player,
                Mode = MoveMode.PathOfConquer,
                SourceArea = movingArmy,
                TargetArea = areaTarget
            };

            // When
            this.sut.Move(moveOperation);

            // Then
            for (int i = 0; i < movingArmy.Units.Count; i++)
            {
                Assert.AreEqual(areaTarget.Units[i].Position, movingArmy.Units[i].Position);
            }
        }

        [Test]
        public void Single_unit_should_reach_five_moving_steps()
        {
            // Given
            Area movingArmy = this.CreateMovingArmy(new[] { new Coordinates(0, 0) }, playerMovePoints: 1000);

            int circleRadius = 1;
            var pathSteps = new List<Area>
            {
                this.Given_CreateCircleArea(new Coordinates(10, 20), circleRadius),
                this.Given_CreateCircleArea(new Coordinates(5, 30), circleRadius),
                this.Given_CreateCircleArea(new Coordinates(40, 30), circleRadius),
                this.Given_CreateCircleArea(new Coordinates(80, 25), circleRadius),
                this.Given_CreateCircleArea(new Coordinates(100, 50), circleRadius)
            };

            foreach (Area step in pathSteps)
            {
                var moveOperation = new MoveOperation
                {
                    Player = this.player,
                    Mode = MoveMode.PathOfConquer,
                    SourceArea = movingArmy,
                    TargetArea = step
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

        private Area Given_CreateCircleArea(Coordinates centerPosition, int areaRadius)
        {
            List<Coordinates> areaPositions = this.CreateCircleShapedUnits(centerPosition, areaRadius);

            List<AreaUnit> units = areaPositions
                .Select(position => new AreaUnit(position))
                .ToList();

            return new Area
            {
                Units = units
            };
        }

        private Area Given_CreateCircleArmy(Coordinates centerPosition, int armyRadius, int playerMovePoints = 100)
        {
            List<Coordinates> unitsPositions = this.CreateCircleShapedUnits(centerPosition, armyRadius);

            return this.CreateMovingArmy(unitsPositions, playerMovePoints);
        }

        private Area CreateMovingArmy(IEnumerable<Coordinates> initPositions, int playerMovePoints = 100)
        {
            IPlayer player = Substitute.For<IPlayer>();
            player.MovePoints = playerMovePoints;

            List<AreaUnit> units = initPositions
                .Select(position => new AreaUnit(position))
                .ToList();

            var targetArea = new Area
            {
                Units = units
            };

            return targetArea;
        }

        private List<Coordinates> CreateCircleShapedUnits(Coordinates centerPosition, int armyRadius)
        {
            // TODO: Extend Circle units generator
            List<Coordinates> unitsPositions = new List<Coordinates> { centerPosition };
            if (armyRadius == 3)
            {
                unitsPositions.Add(new Coordinates(centerPosition.X, centerPosition.Y - 1));
                unitsPositions.Add(new Coordinates(centerPosition.X, centerPosition.Y + 1));
                unitsPositions.Add(new Coordinates(centerPosition.X - 1, centerPosition.Y));
                unitsPositions.Add(new Coordinates(centerPosition.X + 1, centerPosition.Y));
            }

            return unitsPositions;
        }
    }
}
