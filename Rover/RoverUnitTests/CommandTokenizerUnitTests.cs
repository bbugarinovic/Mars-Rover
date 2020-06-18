using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRoverExercise.Tokenizer;
using Moq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MarsRoverExercise;

namespace RoverUnitTests
{
    [TestClass]
    public class CommandTokenizerUnitTests
    {
        private const int CommandLinePlateauNumOfParams = 4;
        private const int CommandLineLandingNumOfParams = 6;
        private const int CommandLineInstructionsNumOfParams = 4;

        private CommandTokenizer _commandTokenizer = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _commandTokenizer = new CommandTokenizer();
        }

        [TestMethod]
        public void WhenParsingPlateauCommand_ExpectPlateauTypeAndTwoNumbers()
        {
            string plateauCommand = "Plateau:5 5";
            var tokenList = _commandTokenizer.Tokenize(plateauCommand);

            Assert.IsTrue(tokenList.Count() == CommandLinePlateauNumOfParams);
            Assert.IsTrue(tokenList.ElementAt(0).TokenType == TokenType.Plateau);
            Assert.IsTrue(tokenList.ElementAt(1).TokenType == TokenType.Number);
            Assert.IsTrue(tokenList.ElementAt(2).TokenType == TokenType.Number);
        }

        [TestMethod]
        public void WhenParsingLandingCommand_ExpectLandingTypeAndPositionParameters()
        {
            string landingCommand = "Rover1 Landing:1 2 N";
            var tokenList = _commandTokenizer.Tokenize(landingCommand);

            Assert.IsTrue(tokenList.Count() == CommandLineLandingNumOfParams);
            Assert.IsTrue(tokenList.ElementAt(1).TokenType == TokenType.Landing);
            Assert.IsTrue(tokenList.ElementAt(2).TokenType == TokenType.Number);
            Assert.IsTrue(tokenList.ElementAt(3).TokenType == TokenType.Number);
            Assert.IsTrue(tokenList.ElementAt(4).TokenType == TokenType.StringValue);
        }

        [TestMethod]
        public void WhenParsingInstructionsCommand_ExpectInstructionAndStringType()
        {
            string instructionsCommand = "Rover1 Instructions:LMLMLMLMM";
            var tokenList = _commandTokenizer.Tokenize(instructionsCommand);

            Assert.IsTrue(tokenList.Count() == CommandLineInstructionsNumOfParams);
            Assert.IsTrue(tokenList.ElementAt(1).TokenType == TokenType.Instructions);
            Assert.IsTrue(tokenList.ElementAt(2).TokenType == TokenType.StringValue);
        }
    }
}
