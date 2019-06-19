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
            startMap = new byte[3, 3] { { 3, 2, 7 }, { 4, 8, 5 }, { 0, 1, 6 } }; // 18
            //startMap = new byte[3, 3] { { 1, 5, 0 }, { 7, 4, 3 }, { 8, 2, 6 } }; // 31

            var start = new Puzzle(startMap);
            var end = new Puzzle();

            var analizer = new PuzzleAnalizer();
            analizer.OnLocateTarget += Analizer_OnFinished;

            var mode = (args.Length > 0) ? args[0] : "";
            var graph = (args.Length > 1) ? args[1] == "graph-mode" : false;
            node = Search(analizer, start, end, mode, graph);

            Console.WriteLine($"Solved: {node != null}");
            Game();
        }

        private static PuzzleNode Search(PuzzleAnalizer analizer, Puzzle start, Puzzle end, string mode, bool graph) 
        {
            switch (mode) 
            {
                case "breadth":
                default:
                    return analizer.SearchBreadth(start, end, graph);
                case "deepth": 
                    return analizer.SearchDeepth(start, end, graph);
                case "uniformcost": 
                    return analizer.SearchByUniformCost(start, end, graph);
                case "greedy":
                    return analizer.SearchGreedy(start, end, graph);
                case "a*":
                    return analizer.SearchAsterisk(start, end, graph);
            }
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

        public static void Print(Puzzle puzzle)
        {
            for (int i = Puzzle.SIZE - 1; i >= 0; i--)
            {
                Console.WriteLine($"[{puzzle.map[i, 0]}] [{puzzle.map[i, 1]}] [{puzzle.map[i, 2]}]");
            }

            Console.WriteLine();
            Console.WriteLine("Moves:");


            for (int i = 0; i < puzzle.Routes.Count; i++)
            {
                var move = puzzle.Routes[i];
                Console.WriteLine($"{i}: [{move.Index}] <- [{move.From.X}, {move.From.Y}] ");
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
                Print(puzzle);

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
