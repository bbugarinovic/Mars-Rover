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
    public class ProgramUnitTests
    {
        private const int DefaultPlateauWidth = 5;
        private const int DefaultPlateauHeight = 5;

        [TestMethod]
        public void WhenProcessingPlateauCommad_RoverSucessfullyCreated()
        {
            var plateauWidth = "7";
            var plateauHeight = "7";
            var plateauCommand = "Plateau:" + plateauWidth + " " + plateauHeight;
            var rover = new MarsRover(DefaultPlateauWidth, DefaultPlateauHeight);
            var commandTokenizerMock = new Mock<ITokenizer>();

            commandTokenizerMock
                .Setup(x => x.Tokenize(plateauCommand))
                .Returns(() => new List<DslToken>() { 
                    new DslToken(TokenType.Plateau, "Plateau:"),
                    new DslToken(TokenType.Number, plateauWidth),
                    new DslToken(TokenType.Number, plateauHeight),
                    new DslToken(TokenType.SequenceTerminator)
                });
            
            MarsRoverExercise.Program.ProcessCommand(ref rover, commandTokenizerMock.Object, plateauCommand, 1);

            Assert.IsTrue("Width: " + plateauWidth + ", Height: " + plateauHeight == rover.CurrentPlateauDimenstions());
        }

        [TestMethod]
        public void WhenProcessingInstructionsCommad_RoverSucessfullyMoved()
        {
            var instructions = "MMM";
            var instructionsCommand = "Rover Insturctions: " + instructions;
            var rover = new MarsRover(DefaultPlateauWidth, DefaultPlateauHeight);
            var commandTokenizerMock = new Mock<ITokenizer>();

            commandTokenizerMock
                .Setup(x => x.Tokenize(instructionsCommand))
                .Returns(() => new List<DslToken>() {
                    new DslToken(TokenType.StringValue, "Rover"),
                    new DslToken(TokenType.Instructions, "Insturctions:"),
                    new DslToken(TokenType.StringValue, instructions),
                    new DslToken(TokenType.SequenceTerminator)
                });

            MarsRoverExercise.Program.ProcessCommand(ref rover, commandTokenizerMock.Object, instructionsCommand, 1);

            Assert.IsTrue("0 3 N" == rover.CurrentPosition());
        }
    }
}
