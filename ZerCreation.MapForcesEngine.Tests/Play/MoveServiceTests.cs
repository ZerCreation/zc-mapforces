using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Operations;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Tests.Play
{
    [TestFixture]
    public class MoveServiceTests
    {
        private IFixture fixture;
        private MoveService sut;

        [SetUp]
        public void Init()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var cartographer = this.fixture.Create<ICartographer>();
            cartographer.FindAreaUnit(Arg.Any<Coordinates>()).Returns(new AreaUnit(0, 0));

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
            Army movingArmy = this.Given_CreateCircleArmy(new Coordinates(armyX, armyY), circleRadius);
            Area areaTarget = this.Given_CreateCircleArea(new Coordinates(areaX, areaY), circleRadius);

            var moveOperation = new MoveOperation
            {
                Mode = MoveMode.Basic,
                MovingArmy = movingArmy,
                AreaTarget = areaTarget
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
            Army movingArmy = this.CreateMovingArmy(new[] { new Coordinates(0, 0) }, playerMovePoints: 1000);

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

        private Army Given_CreateCircleArmy(Coordinates centerPosition, int armyRadius, int playerMovePoints = 100)
        {
            List<Coordinates> unitsPositions = this.CreateCircleShapedUnits(centerPosition, armyRadius);

            return this.CreateMovingArmy(unitsPositions, playerMovePoints);
        }

        private Army CreateMovingArmy(IEnumerable<Coordinates> initPositions, int playerMovePoints = 100)
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
