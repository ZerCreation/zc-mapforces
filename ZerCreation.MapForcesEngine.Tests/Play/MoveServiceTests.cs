using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.NUnit3;
using NSubstitute;
using NUnit.Framework;
using System;
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
            cartographer.FindAreaUnit(Arg.Any<Coordinates>())
                .Returns(args => new AreaUnit(((Coordinates)args[0]).X, ((Coordinates)args[0]).Y));

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
            Area sourceArea = this.Given_CreateCircleArmy(new Coordinates(armyX, armyY), circleRadius);
            Area targetArea = this.Given_CreateCircleArea(new Coordinates(areaX, areaY), circleRadius);

            var moveOperation = new MoveOperation
            {
                Player = this.player,
                Mode = MoveMode.PathOfConquer,
                SourceArea = sourceArea,
                TargetArea = targetArea
            };

            // When
            IEnumerable<HashSet<AreaUnit>> moveResults = this.sut.Move(moveOperation);
            List<AreaUnit> finalMoveResult = moveResults.Last().ToList();

            // Then
            for (int i = 0; i < sourceArea.Units.Count; i++)
            {
                Assert.AreEqual(targetArea.Units[i].Position, finalMoveResult[i].Position);
            }
        }

        [Test]
        public void Single_unit_should_reach_five_moving_steps()
        {
            // Given
            const int circleRadius = 1;
            var pathSteps = new List<Area>
            {
                this.Given_CreateCircleArea(new Coordinates(10, 20), circleRadius),
                this.Given_CreateCircleArea(new Coordinates(5, 30), circleRadius),
                this.Given_CreateCircleArea(new Coordinates(40, 30), circleRadius),
                this.Given_CreateCircleArea(new Coordinates(80, 25), circleRadius),
                this.Given_CreateCircleArea(new Coordinates(100, 50), circleRadius)
            };

            Area sourceStepArea = this.CreateArmy(new[] { new Coordinates(0, 0) }, playerMovePoints: 1000);

            foreach (Area targetStepArea in pathSteps)
            {
                var moveOperation = new MoveOperation
                {
                    Player = this.player,
                    Mode = MoveMode.PathOfConquer,
                    SourceArea = sourceStepArea,
                    TargetArea = targetStepArea
                };

                // When
                IEnumerable<HashSet<AreaUnit>> moveResults = this.sut.Move(moveOperation);

                // Then
                AreaUnit finalMoveResultUnit = moveResults.Last().Single();
                AreaUnit targetAreaUnit = targetStepArea.Units.Single();
                Assert.AreEqual(targetAreaUnit.Position, finalMoveResultUnit.Position);

                sourceStepArea = targetStepArea;
            }
        }

        [Test]
        [AutoData]
        public void Single_unit_should_reach_random_target(Coordinates sourcePosition, Coordinates targetPosition)
        {
            //sourcePosition = new Coordinates(255, 72);
            //targetPosition = new Coordinates(12, 189);

            // Given
            Console.WriteLine(sourcePosition);
            Console.WriteLine(targetPosition);

            int circleRadius = 1;
            Area sourceArea = this.Given_CreateCircleArmy(sourcePosition, circleRadius);
            Area targetArea = this.Given_CreateCircleArea(targetPosition, circleRadius);

            var moveOperation = new MoveOperation
            {
                Player = this.player,
                Mode = MoveMode.PathOfConquer,
                SourceArea = sourceArea,
                TargetArea = targetArea
            };

            // When
            IEnumerable<HashSet<AreaUnit>> moveResults = this.sut.Move(moveOperation);
            List<AreaUnit> finalMoveResult = moveResults.Last().ToList();

            // Then
            Assert.AreEqual(targetArea.Units.Single().Position, finalMoveResult.Single().Position);
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

        private Area Given_CreateCircleArmy(Coordinates centerPosition, int armyRadius, int playerMovePoints = int.MaxValue)
        {
            List<Coordinates> unitsPositions = this.CreateCircleShapedUnits(centerPosition, armyRadius);

            return this.CreateArmy(unitsPositions, playerMovePoints);
        }

        private Area CreateArmy(IEnumerable<Coordinates> initPositions, int playerMovePoints = 100)
        {
            IPlayer player = Substitute.For<IPlayer>();
            player.MovePoints = playerMovePoints;

            var sourceArea = new Area
            {
                Units = initPositions
                    .Select(position => new AreaUnit(position))
                    .ToList()
            };

            return sourceArea;
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
