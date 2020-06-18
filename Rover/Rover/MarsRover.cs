using System;

namespace MarsRoverExercise
{
    public enum RoverDirection
    {
        North = 'N',
        East = 'E',
        South = 'S',
        West = 'W'
    }

    public enum RoverCommand
    {
        Left = 'L',
        Right = 'R',
        Move = 'M'
    }

    public class MarsRover
    {
        private int x = 0;
        private int y = 0;
        private RoverDirection facing = RoverDirection.North;
        private readonly int plateauWidth;
        private readonly int plateauHeight;
        
        public string Name
        { get; set; }

        public MarsRover(int width, int height)
        {
            plateauWidth = width;
            plateauHeight = height;
            Name = "Rover";
        }

        public void SetPosition(int x, int y, RoverDirection facing)
        {
            this.x = x;
            this.y = y;
            this.facing = facing;
            CheckIfOutOfBounds();
        }

        public string CurrentPosition()
        {
            return x +  " " +  y +  " " + (char)facing;
        }

        public string CurrentPlateauDimenstions()
        {
            return "Width: " + plateauWidth + ", Height: " + plateauHeight;
        }

        public void ProcessCommands(string commands)
        {
            var commandChars = commands.ToCharArray();

            for (int idx = 0; idx < commandChars.Length; idx++)
            {
                ProcessCommand(commandChars[idx]);
            }
        }

        private void ProcessCommand(char command)
        {
            switch ((RoverCommand)command)
            {
                case RoverCommand.Move:
                    Move();
                    break;
                case RoverCommand.Left:
                    TurnLeft();
                    break;
                case RoverCommand.Right:
                    TurnRight();
                    break;
                default:
                    Console.WriteLine($"Invalid Command Character {command}");
                    break;
            }

            CheckIfOutOfBounds();
        }

        private void CheckIfOutOfBounds()
        { 
            if (this.x < 0 || this.x > plateauWidth || this.y < 0 || this.y > plateauHeight)
            {
                throw new Exception($"Position can not be beyond bounderies (0 , 0) and ({plateauWidth} , {plateauHeight})");
            }
        }

        private void Move()
        {
            switch (facing)
            {
                case RoverDirection.North:
                    y++;
                    break;
                case RoverDirection.South:
                    y--;
                    break;
                case RoverDirection.East:
                    x++;
                    break;
                case RoverDirection.West:
                    x--;
                    break;
                default:
                    break;
            }
        }

        private void TurnLeft()
        {
            switch (facing)
            {
                case RoverDirection.North:
                    facing = RoverDirection.West;
                    break;
                case RoverDirection.South:
                    facing = RoverDirection.East;
                    break;
                case RoverDirection.East:
                    facing = RoverDirection.North;
                    break;
                case RoverDirection.West:
                    facing = RoverDirection.South;
                    break;
                default:
                    break;
            }
        }

        private void TurnRight()
        {
            switch (facing)
            {
                case RoverDirection.North:
                    facing = RoverDirection.East;
                    break;
                case RoverDirection.South:
                    facing = RoverDirection.West;
                    break;
                case RoverDirection.East:
                    facing = RoverDirection.South;
                    break;
                case RoverDirection.West:
                    facing = RoverDirection.North;
                    break;
                default:
                    break;
            }
        }
    }
}
