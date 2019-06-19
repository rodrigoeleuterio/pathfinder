using Eleutherius.Genesis.SlidingPuzzle;
using Eleutherius.Genesis.SlidingPuzzle.AI;
using System;

namespace Eleutherius.Genesis.Terminal
{
    class Program
    {
        private static byte[,] startMap;
        private static PuzzleNode node;
        private static string result;

        static void Main(string[] args)
        {
            Console.Clear();
            //startMap = new byte[3, 3] { { 3, 2, 7 }, { 4, 8, 5 }, { 0, 1, 6 } }; // 18
            startMap = new byte[3, 3] { { 1, 5, 0 }, { 7, 4, 3 }, { 8, 2, 6 } }; // 31

            var start = new Puzzle(startMap);
            var end = new Puzzle();

            var analizer = new PuzzleAnalizer();
            analizer.OnFinished += Analizer_OnFinished;

            var mode = (args.Length > 0) ? args[0] : "";

            switch (mode) {
                case "deepth": 
                    node = analizer.SearchDeepth(start, end);
                    break;
                case "greedy":
                    node = analizer.SearchGreedy(start, end);
                    break;
                case "heuristic":
                    node = analizer.SearchByHeuristic(start, end);
                    break;
                default: 
                    node = analizer.SearchBreadth(start, end);
                    break;
            }

            Console.WriteLine($"Solved: {node != null}");
            Game();
        }

        private static void Analizer_OnFinished(string obj)
        {
            result = obj;
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
                Console.Clear();

                Console.WriteLine(result);
                PrintMovement();
                puzzle.Print();

                move = ReadMove();

                if (move == 0) break;
                if (move == -1) continue;

                while (!puzzle.Move(move))
                {
                    Console.WriteLine("Movement not allowed!");
                }
            }
            while (true);
        }

        private static int ReadMove()
        {
            Console.Write("Enter a number (press 0 to exit): ");
            string input = Console.ReadLine();
            return int.TryParse(input, out int key) ? key : -1;
        }
    }
}
