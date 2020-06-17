using System;
using System.IO;
using System.Linq;

namespace MarsRover
{
    enum CommandType
    {
        Plateau,
        Landing,
        Instructions
    }

    class Program
    {
        private const char commandTypeSeparator = ':';
        private const int defaultPlateauWidth = 5;
        private const int defaultPlateauHeight = 5;
        private const int commandLineElements = 2;
        private const int commandLinePlateauNumOfParams = 2;
        private const int commandLineLandingNumOfParams = 3;

        static void Main(string[] args)
        {
            Rover rover = new Rover(defaultPlateauWidth, defaultPlateauHeight);

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
                            var parameters = line.Split(commandTypeSeparator);
                            lineNumber++;

                            if (parameters.Length != commandLineElements)
                            {
                                throw new Exception("Invalid input file at line " + lineNumber);
                            }

                            if (parameters[0].Contains(CommandType.Plateau.ToString()))
                            {
                                var plateauParams = parameters[1].Split(' ');

                                if (plateauParams.Length != commandLinePlateauNumOfParams)
                                {
                                    throw new Exception("Invalid input file at line " + lineNumber);
                                }

                                rover = new Rover(Convert.ToInt32(plateauParams[0]),
                                    Convert.ToInt32(plateauParams[1]));
                            }
                            else if (parameters[0].Contains(CommandType.Landing.ToString()))
                            {
                                var landingParams = parameters[1].Split(' ');

                                if (landingParams.Length != commandLineLandingNumOfParams)
                                {
                                    throw new Exception("Invalid input file at line " + lineNumber);
                                }

                                rover.setPosition(Convert.ToInt32(landingParams[0]),
                                    Convert.ToInt32(landingParams[1]),
                                    (RoverDirection)landingParams[2][0]);
                            }
                            else if (parameters[0].Contains(CommandType.Instructions.ToString()))
                            {
                                if (!IsInstructionStringValid(parameters[1]))
                                {
                                    throw new Exception("Invalid input file at line " + lineNumber);
                                }

                                rover.process(parameters[1]);
                                Console.WriteLine(rover.currentPosition());
                            }
                        }
                    }
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
