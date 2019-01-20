using NUnit.Framework;
using System.Collections.Generic;
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
            var moveOperation = new MoveOperation
            {
                Mode = MoveMode.Basic,
                MovingArmy = new Army()
                {
                    Units = new List<MovingUnit>
                    {
                        new MovingUnit
                        {
                            Position = new Coordinates
                            {
                                X = 10,
                                Y = 10
                            }
                        }
                    }
                },
                AreaTarget = new Area
                {
                    Units = new List<AreaUnit>
                    {
                        new AreaUnit
                        {
                            Position = new Coordinates
                            {
                                X = 20,
                                Y = 20
                            }
                        }
                    }
                }
            };

            // When
            this.sut.SolveMove(moveOperation);

            // Then
            
        }
    }
}
