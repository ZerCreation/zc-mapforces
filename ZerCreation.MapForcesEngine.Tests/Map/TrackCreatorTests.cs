using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Enums;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Move;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Tests.Map
{
    public class TrackCreatorTests
    {
        private IFixture fixture;
        private TrackCreator sut;

        [SetUp]
        public void Init()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            this.sut = new TrackCreator();
        }

        [Test]
        public void Should_prepare_simple_move_path_without_any_exception()
        {
            // Given
            var moveOperation = new MoveOperation
            {
                Mode = MoveMode.PathOfConquer,
                SourceArea = new Area
                {
                    Units = new List<AreaUnit>
                    {
                        new AreaUnit(10, 10)
                    }
                },
                TargetArea = new Area
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

        [Test]
        public void Should_throw_when_5_MovingUnits_move_to_3_AreaUnits_in_Basic_mode()
        {
            // Given
            var moveOperation = new MoveOperation
            {
                Mode = MoveMode.PathOfConquer,
                SourceArea = new Area
                {
                    Units = this.fixture.CreateMany<AreaUnit>(3).ToList()
                },
                TargetArea = new Area
                {
                    Units = this.fixture.CreateMany<AreaUnit>(5).ToList()
                }
            };

            // When
            TestDelegate solveDelegate = () => this.sut.SetupMovePaths(moveOperation);

            // Then
            Assert.Throws<ArgumentException>(solveDelegate);
        }
    }
}
