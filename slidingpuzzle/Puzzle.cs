using Eleutherius.Genesis.Core;
using Eleutherius.Genesis.Pathfinder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Eleutherius.Genesis.SlidingPuzzle
{
    public class Puzzle : IEnvironment<PuzzleRoute>
    {
        private delegate void PuzzleProcess(Point position);

        public const int SIZE = 3;
        public const int MAX = SIZE - 1;
        public const int AREA = SIZE * SIZE;

        private readonly static Point[] points = new Point[] { new Point(0, 0),  new Point(0, 1), new Point(0, 2), new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(2, 0), new Point(2, 1), new Point(2, 2) };

        public readonly byte[,] map;
        public readonly Point[] mapIndexed = new Point[AREA];
        private int hashcode = 0;
        private Point Zero { get => mapIndexed[0]; }

        public Puzzle(byte[,] startMap = null)
        {
            map = new byte[SIZE, SIZE];
            if (startMap == null)
            {
                startMap = new byte[,] {{0, 1, 2}, {3, 4, 5}, {6, 7, 8}};
            }
            Reset(startMap);
        }

        private Puzzle(Puzzle puzzle)
        {
            hashcode = puzzle.hashcode;

            map = new byte[SIZE, SIZE];
            for (int i = 0; i < AREA; i++) 
            {
                var position = points[i];
                SetValue(position, puzzle.map[position.X, position.Y]);
            }

            Routes = new List<PuzzleRoute>();
            foreach (var move in puzzle.Routes) Routes.Add((PuzzleRoute) move.Clone());           
        }
        /**
         * ## PROPERTIES ##
         **/
        public List<PuzzleRoute> Routes { get; private set; }


        /**
         * ## METHODS ##
         **/
        #region methods
        public bool Move(int index) => Move(Routes.FirstOrDefault(m => m.Index == index));

        public bool Move(PuzzleRoute movement)
        {
            if (IsNotValidMovement(movement)) return false;

            SetValue(movement.To, map[movement.From.X, movement.From.Y]);
            SetValue(movement.From, 0);

            CalculateHashCode();
            CalculateMoves();

            return true;
        }
        
        public void Print()
        {
            for (int i = SIZE - 1; i >= 0; i--)
            {
                Console.WriteLine($"[{map[i, 0]}] [{map[i, 1]}] [{map[i, 2]}]");
            }

            Console.WriteLine();
            Console.WriteLine("Moves:");


            for (int i = 0; i < Routes.Count; i++)
            {
                var move = Routes[i];
                Console.WriteLine($"{i}: [{move.Index}] <- [{move.From.X}, {move.From.Y}] ");
            }
        }

        public override int GetHashCode() => hashcode;

        public override bool Equals(object obj) => obj is Puzzle puzzle && Equals(puzzle);

        public bool Equals(Puzzle puzzle)
        {
            if (HashNotEqual(puzzle.hashcode)) return false;
            if (DimensionsNotEquals(puzzle.map)) return false;
            return Equals(puzzle.map);
        }
        #endregion


        /**
         * ## PRIVATE METHODS ##
         **/
        #region private
        private void Reset(byte[,] startMap)
        {
            for (int i = 0; i < AREA; i++) 
            {
                var position = points[i];
                SetValue(position, startMap[position.X, position.Y]);
            }
            
            CalculateHashCode();
            CalculateMoves();
        }

        private bool Equals(byte[,] map)
        {
            for (int i = 0; i < AREA; i++) 
            {
                var position = points[i];
                if (this.map[position.X, position.Y] != map[position.X, position.Y]) return false;
            }
            return true;
        }

        private bool HashNotEqual(int hashcode) => this.hashcode != hashcode;

        private bool DimensionsNotEquals(byte[,] map) => this.map.GetLength(0) != map.GetLength(0) || this.map.GetLength(1) != map.GetLength(1);

        private bool IsNotValidMovement(PuzzleRoute move) => move == null || !IsValid(move.From) || !IsValid(move.To) || move.ManhattanDistance > 1 || !Zero.Equals(move.To);

        private void CalculateMoves()
        {
            Routes = new List<PuzzleRoute>();
            AddMovement(new Point(Zero.X, Zero.Y + 1));
            AddMovement(new Point(Zero.X, Zero.Y - 1));
            AddMovement(new Point(Zero.X + 1, Zero.Y));
            AddMovement(new Point(Zero.X - 1, Zero.Y));
        }

        private void AddMovement(Point from)
        {
            if (IsValid(from)) Routes.Add(new PuzzleRoute() { From = from, To = Zero, Index = map[from.X, from.Y] });
        }

        private static bool IsValid(Point point) => point.X.Between(0, MAX) && point.Y.Between(0, MAX);

        private void SetValue(Point position, int value) {
            map[position.X, position.Y] = (byte) value;
            mapIndexed[value] = position;
        }

        private void CalculateHashCode()
        {
            ClearHash();
            CalculateHash(Normalize());
        }

        private int[] Normalize()
        {
            int[] normalized = new int[AREA];
            for (int i = 0; i < AREA; i++) 
            {
                var position = mapIndexed[i];
                var value = map[position.X, position.Y];
                normalized[i] = value - i;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (normalized[j] > value) normalized[i]++;
                }
            }
            return normalized;
        }

        private static int GetIndex(int x, int y) => x * SIZE + y;

        private void CalculateHash(int[] normalized)
        {
            for (int i = 0; i < AREA; i++)
            {
                var factor = AREA - i - 1;
                hashcode += normalized[i] * (int)Factorial.GetFactorial(factor);
            }
        }

        private void ClearHash() => hashcode = 0;       

        public object Clone() => new Puzzle(this);
        #endregion
    }
}