using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.Arbiters;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Operations;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Tests.Arbiters
{
    [TestFixture]
    public class ArbiterDispatcherTests
    {
        private ArbiterDispatcher sut;

        [SetUp]
        public void Init()
        {
            this.sut = new ArbiterDispatcher();
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
            List<MovingUnit> armyUnits = new List<MovingUnit>
            {
                new MovingUnit
                {
                    Position = new Coordinates(armyX, armyY)
                }
            };

            List<AreaUnit> areaUnits = new List<AreaUnit>
            {
                new AreaUnit
                {
                    Position = new Coordinates(areaX, areaY)
                }
            };

            Player player = Substitute.For<Player>();
            player.MovePoints = 100;

            var moveOperation = new MoveOperation
            {
                Mode = MoveMode.Basic,
                MovingArmy = new Army()
                {
                    PlayerPossesion = player,
                    Units = armyUnits
                },
                AreaTarget = new Area
                {
                    Units = areaUnits
                }
            };

            // When
            this.sut.SolveMove(moveOperation);

            // Then
            Assert.AreEqual(areaUnits.Single().Position, armyUnits.Single().Position);
        }
    }
}
