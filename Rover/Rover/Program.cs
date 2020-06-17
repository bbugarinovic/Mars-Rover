using System;
using System.Linq;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            var maxPoints = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToList();
            var startPositions = Console.ReadLine().Trim().Split(' ');
            Rover rover = new Rover(maxPoints[0], maxPoints[1]);

            if (startPositions.Count() == 3)
            {
                rover.setPosition(Convert.ToInt32(startPositions[0]),
                    Convert.ToInt32(startPositions[1]),
                    (RoverDirection)Enum.Parse(typeof(RoverDirection), startPositions[2]));
            }

            var moves = Console.ReadLine().ToUpper();

            try
            {
                rover.process(moves);
                Console.WriteLine(rover.currentPosition());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();

            /*
            Rover rover = new Rover(5, 5);

            rover.setPosition(1, 2, RoverDirection.North);
            rover.process("LMLMLMLMM");
            rover.printPosition(); // prints 1 3 N
            rover.setPosition(3, 3, RoverDirection.East);
            rover.process("MMRMMRMRRM");
            rover.printPosition(); // prints 5 1 E
            */
        }
    }
}
