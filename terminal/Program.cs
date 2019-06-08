using Genesis.SlidingPuzzle;
using Genesis.SlidingPuzzle.AI;
using System;

namespace Genesis.Terminal
{
    class Program
    {
        private static byte[,] startMap;

        static void Main(string[] args)
        {
            startMap = new byte[3, 3] { { 3, 2, 7 }, { 4, 8, 5 }, { 0, 1, 6 } };

            var start = new Puzzle(startMap);
            var end = new Puzzle();

            var analizer = new PuzzleAnalizer();

            var node = analizer.SearchByExtension(start, end);

            Console.WriteLine($"Solved: {node != null}");
            if (node != null)
            {
                Console.WriteLine($"Movements: {node.Deep}");

                while (node != null && node.Deep > 0)
                {
                    Console.Write($"[{node.Movement.Index}]");
                    node = (PuzzleNode) node.Parent;
                }

                Console.WriteLine();
            }

            Game();
        }

        private static void Game()
        {
            Puzzle puzzle = new Puzzle(startMap);
            int move;
            do
            {
                puzzle.Print();

                move = ReadMove();

                while (!puzzle.Move(move))
                {
                    Console.WriteLine("Movement not allowed!");
                    move = ReadMove();
                }

                Console.Clear();
            }
            while (move > 0);
        }

        private static int ReadMove()
        {
            Console.Write("Enter a number (press 0 to exit): ");
            return int.Parse(Console.ReadLine());
        }
    }
}
