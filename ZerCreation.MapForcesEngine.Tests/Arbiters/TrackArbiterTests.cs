using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using ZerCreation.MapForcesEngine.Arbiters;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Operations;
using ZerCreation.MapForcesEngine.Play;

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
        public void Should_prepare_simple_move_path_without_any_exception()
        {
            // Given
            var moveOperation = new MoveOperation
            {
                Mode = MoveMode.Basic,
                MovingArmy = new Army()
                {
                    PlayerPossesion = Substitute.For<Player>(),
                    Units = new List<MovingUnit>
                    {
                        new MovingUnit
                        {
                            Position = new Coordinates(10, 10)
                        }
                    }
                },
                AreaTarget = new Area
                {
                    Units = new List<AreaUnit>
                    {
                        new AreaUnit
                        {
                            Position = new Coordinates(20, 20)
                        }
                    }
                }
            };

            // When
            TestDelegate solveDelegate = () => this.sut.SolveMove(moveOperation);

            // Then
            Assert.DoesNotThrow(solveDelegate);
        }
    }
}
