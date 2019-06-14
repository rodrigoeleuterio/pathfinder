using Genesis.SlidingPuzzle;
using Genesis.SlidingPuzzle.AI;
using System;

namespace Genesis.Terminal
{
    class Program
    {
        private static byte[,] startMap;
        private static PuzzleNode node;

        static void Main(string[] args)
        {
            Console.Clear();
            startMap = new byte[3, 3] { { 3, 2, 7 }, { 4, 8, 5 }, { 0, 1, 6 } };

            var start = new Puzzle(startMap);
            var end = new Puzzle();

            var analizer = new PuzzleAnalizer();

            var mode = (args.Length > 0) ? args[0] : "";

            switch (mode) {
                case "deepth": 
                    node = analizer.DepthSearch(start, end);
                    break;
                default: 
                    node = analizer.BreadthSearch(start, end);
                    break;
            }

            Console.WriteLine($"Solved: {node != null}");
            Game();
        }

        private static void PrintMovement() {
            if (node != null)
            {
                Console.WriteLine($"Movements: {node.Deep}");

                var parent = node;
                while (parent != null && parent.Deep > 0)
                {
                    Console.Write($"[{parent.Movement.Index}]");
                    parent = (PuzzleNode) parent.Parent;
                }

                Console.WriteLine();
            }
        }

        private static void Game()
        {
            Puzzle puzzle = new Puzzle(startMap);
            int move;
            do
            {
                PrintMovement();
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
