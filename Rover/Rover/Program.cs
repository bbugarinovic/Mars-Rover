﻿using System;
using System.IO;
using System.Linq;
using MarsRoverExercise.Tokenizer;

namespace MarsRoverExercise
{
    class Program
    {
        private const int defaultPlateauWidth = 5;
        private const int defaultPlateauHeight = 5;
        private const int commandLinePlateauNumOfParams = 4;
        private const int commandLineLandingNumOfParams = 6;

        static void Main(string[] args)
        {
            MarsRover rover = new MarsRover(defaultPlateauWidth, defaultPlateauHeight);

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

                            CommandTokenizer tokenizer = new CommandTokenizer();
                            var tokenList = tokenizer.Tokenize(line);

                            if (tokenList.Count() < 2)
                            {
                                throw new Exception("Invalid input file at line " + lineNumber);
                            }

                            if (tokenList.ElementAt(0).TokenType == TokenType.Plateau)
                            {
                                if (tokenList.Count() != commandLinePlateauNumOfParams)
                                {
                                    throw new Exception("Invalid input for Plateau at line " + lineNumber);
                                }

                                rover = new MarsRover(Convert.ToInt32(tokenList.ElementAt(1).Value),
                                    Convert.ToInt32(tokenList.ElementAt(2).Value));
                            }
                            else if (tokenList.ElementAt(1).TokenType == TokenType.Landing)
                            {
                                if (tokenList.Count() != commandLineLandingNumOfParams)
                                {
                                    throw new Exception("Invalid input for Landing at line " + lineNumber);
                                }

                                rover.Name = tokenList.ElementAt(0).Value;
                                rover.setPosition(Convert.ToInt32(tokenList.ElementAt(2).Value),
                                    Convert.ToInt32(tokenList.ElementAt(3).Value),
                                    (RoverDirection)tokenList.ElementAt(4).Value[0]);
                            }
                            else if (tokenList.ElementAt(1).TokenType == TokenType.Instructions)
                            {
                                if (!IsInstructionStringValid(tokenList.ElementAt(2).Value) ||
                                    rover.Name != tokenList.ElementAt(0).Value)
                                {
                                    throw new Exception("Invalid input for Instructions at line " + lineNumber);
                                }

                                rover.process(tokenList.ElementAt(2).Value);
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
