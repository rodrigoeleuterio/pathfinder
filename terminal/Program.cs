using PuzzleGame;
using System;

namespace Pathfinder.App.Terminal
{
    class Program
    {
        private static byte[,] arrange;

        static void Main(string[] args)
        {
            arrange = new byte[3, 3] { { 3, 2, 7 }, { 4, 8, 5 }, { 0, 1, 6 } };
            //var start = new Puzzle(new byte[3, 3] { { 3, 2, 7 }, { 4, 8, 5 }, { 0, 1, 6 } });
            var start = new Puzzle(arrange);
            var end = new Puzzle();

            var solver = new PuzzleSolver();

            var node = solver.SearchByExtension(start, end);

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
            Puzzle puzzle = new Puzzle(arrange);
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
