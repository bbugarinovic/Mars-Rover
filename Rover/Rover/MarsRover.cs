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
        private readonly int gridWidth;
        private readonly int gridHeight;

        public MarsRover(int width, int height)
        {
            gridWidth = width;
            gridHeight = height;
        }

        public string Name   // property
        { get; set; }

        public void setPosition(int x, int y, RoverDirection facing)
        {
            this.x = x;
            this.y = y;
            this.facing = facing;
        }

        public string currentPosition()
        {
            return x +  " " +  y +  " " + (char)facing;
        }

        public void process(string commands)
        {
            var commandChars = commands.ToCharArray();

            for (int idx = 0; idx < commandChars.Length; idx++)
            {
                process(commandChars[idx]);
            }
        }

        private void process(char command)
        {
            switch ((RoverCommand)command)
            {
                case RoverCommand.Move:
                    move();
                    break;
                case RoverCommand.Left:
                    turnLeft();
                    break;
                case RoverCommand.Right:
                    turnRight();
                    break;
                default:
                    Console.WriteLine($"Invalid Command Character {command}");
                    break;
            }

            if (this.x < 0 || this.x > gridWidth || this.y < 0 || this.y > gridHeight)
            {
                throw new Exception($"Position can not be beyond bounderies (0 , 0) and ({gridWidth} , {gridHeight})");
            }
        }

        private void move()
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

        private void turnLeft()
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

        private void turnRight()
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
