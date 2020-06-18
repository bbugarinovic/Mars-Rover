using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRoverExercise;
using System;

namespace RoverUnitTests
{
    [TestClass]
    public class RoverUnitTests
    {
        private const int DefaultGirdWidth = 5;
        private const int DefaultGirdHeight = 5;

        private MarsRover _rover = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _rover = new MarsRover(DefaultGirdWidth, DefaultGirdHeight);
        }

        [TestMethod]
        public void WhenStarting12N_AndMoveCommandLMLMLMLMM_AssertSuccesfulMove()
        {
            _rover.SetPosition(1, 2, RoverDirection.North);

            var moves = "LMLMLMLMM";
            _rover.ProcessCommands(moves);

            var expectedPosition = "1 3 N";
            Assert.AreEqual(expectedPosition, _rover.CurrentPosition());
        }

        [TestMethod]
        public void WhenStarting33E_AndMoveCommandMRRMMRMRRM_AssertSuccesfulMove()
        {
            _rover.SetPosition(3, 3, RoverDirection.East);

            var moves = "MMRMMRMRRM";
            _rover.ProcessCommands(moves);

            var expectedPosition = "5 1 E";
            Assert.AreEqual(expectedPosition, _rover.CurrentPosition());
        }

        [TestMethod]
        public void WhenStarting33E_AndMoveCommandMMM_AssertException()
        {
            _rover.SetPosition(3, 3, RoverDirection.East);

            try
            {
                var moves = "MMM";
                _rover.ProcessCommands(moves);

                Assert.Fail("An exception should have been thrown");
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Position can not be beyond bounderies (0 , 0) and (" + DefaultGirdWidth + " , " + DefaultGirdHeight + ")", exception.Message);
            }
        }

        [TestMethod]
        public void WhenStarting33S_AndMoveCommandMMMMM_AssertException()
        {
            _rover.SetPosition(3, 3, RoverDirection.South);

            try
            {
                var moves = "MMMMM";
                _rover.ProcessCommands(moves);

                Assert.Fail("An exception should have been thrown");
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Position can not be beyond bounderies (0 , 0) and (" + DefaultGirdWidth + " , " + DefaultGirdHeight + ")", exception.Message);
            }
        }

        [TestMethod]
        public void WhenLandingOutOfBounds_AssertException()
        {
            try
            {
                _rover.SetPosition(7, 7, RoverDirection.South);

                Assert.Fail("An exception should have been thrown");
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Position can not be beyond bounderies (0 , 0) and (" + DefaultGirdWidth + " , " + DefaultGirdHeight + ")", exception.Message);
            }
        }
    }
}
