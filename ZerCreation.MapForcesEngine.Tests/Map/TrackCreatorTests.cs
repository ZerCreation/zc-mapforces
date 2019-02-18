using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Operations;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Tests.Map
{
    public class TrackCreatorTests
    {
        private TrackCreator sut;

        [SetUp]
        public void Init()
        {
            this.sut = new TrackCreator();
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
                        new MovingUnit(10, 10)
                    }
                },
                AreaTarget = new Area
                {
                    Units = new List<AreaUnit>
                    {
                        new AreaUnit(20, 20)
                    }
                }
            };

            // When
            TestDelegate solveDelegate = () => this.sut.SetupMovePaths(moveOperation);

            // Then
            Assert.DoesNotThrow(solveDelegate);
        }
    }
}
