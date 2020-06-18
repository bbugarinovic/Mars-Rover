using System;
using System.IO;
using System.Linq;
using MarsRoverExercise.Tokenizer;

namespace MarsRoverExercise
{
    // The following assertions were made:
    // - default plateau size is 5 x 5
    // - default Rover position is (0, 0) facing North
    // - blank input lines are skipped
    // - if rower position is set or moved out of bound, there is an exception thrown
    // - we can have consecutive instruction commands

    public class Program
    {
        private const int DefaultPlateauWidth = 5;
        private const int DefaultPlateauHeight = 5;
        private const int CommandLinePlateauNumOfParams = 4;
        private const int CommandLineLandingNumOfParams = 6;
        private const int CommandLineInstructionsNumOfParams = 4;

        static void Main(string[] args)
        {
            MarsRover rover = new MarsRover(DefaultPlateauWidth, DefaultPlateauHeight);

            if (args != null && args.Length > 0)
            {
                if (File.Exists(args[0]))
                {
                    using (StreamReader file = new StreamReader(args[0]))
                    {
                        string line;
                        int lineNumber = 0;

                        while ((line = file.ReadLine()) != null)
                        {
                            lineNumber++;

                            if (string.IsNullOrEmpty(line))
                            {
                                continue;
                            }

                            ProcessCommand(ref rover, new CommandTokenizer(), line, lineNumber);
                        }
                    }
                }
            }
        }

        public static void ProcessCommand(ref MarsRover rover, ITokenizer tokenizer, string line, int lineNumber)
        {
            var tokenList = tokenizer.Tokenize(line);

            if (tokenList.Count() < 2)
            {
                throw new Exception("Invalid input file at line " + lineNumber);
            }

            if (tokenList.ElementAt(0).TokenType == TokenType.Plateau)
            {
                if (tokenList.Count() != CommandLinePlateauNumOfParams)
                {
                    throw new Exception("Invalid input for Plateau at line " + lineNumber);
                }

                rover = new MarsRover(Convert.ToInt32(tokenList.ElementAt(1).Value),
                    Convert.ToInt32(tokenList.ElementAt(2).Value));
            }
            //
            // Assumption: we can use a default plateau size
            else if (tokenList.ElementAt(1).TokenType == TokenType.Landing)
            {
                if (tokenList.Count() != CommandLineLandingNumOfParams)
                {
                    throw new Exception("Invalid input for Landing at line " + lineNumber);
                }

                rover.Name = tokenList.ElementAt(0).Value;
                rover.SetPosition(Convert.ToInt32(tokenList.ElementAt(2).Value),
                    Convert.ToInt32(tokenList.ElementAt(3).Value),
                    (RoverDirection)tokenList.ElementAt(4).Value[0]);
            }
            //
            // Assumption: we can have multiple insturctions commands, as long as
            // as the rover name is matching the current rover instance
            else if (tokenList.ElementAt(1).TokenType == TokenType.Instructions)
            {
                if (tokenList.Count() != CommandLineInstructionsNumOfParams ||
                    !IsInstructionStringValid(tokenList.ElementAt(2).Value) ||
                    rover.Name != tokenList.ElementAt(0).Value)
                {
                    throw new Exception("Invalid input for Instructions at line " + lineNumber);
                }

                try
                {
                    rover.ProcessCommands(tokenList.ElementAt(2).Value);
                    Console.WriteLine(rover.Name + ":" + rover.CurrentPosition());
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Exception when proccessing commands for " + rover.Name + ":" + exception.Message);
                }
            }
        }

        private static bool IsInstructionStringValid(string instructions)
        {
            string allowableLetters = "LRM";

            foreach (char c in instructions)
            {
                if (!allowableLetters.Contains(c.ToString()))
                    return false;
            }

            return true;
        }
    }
}
