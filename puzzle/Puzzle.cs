using Pathfinder.Core;
using Pathfinder.Core.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PuzzleGame
{
    public class Puzzle : IEnvironment<PuzzleMovement>
    {
        private delegate void PuzzleProcess(int x, int y);

        private const int SIZE = 3;
        private const int MAX = SIZE - 1;
        private const int AREA = SIZE * SIZE;

        private readonly byte[,] arrange;
        private int hashcode = 0;
        private Point zero;

        public Puzzle(byte[,] start = null)
        {
            arrange = new byte[SIZE, SIZE];
            Reset(start);
        }

        private Puzzle(Puzzle puzzle)
        {
            hashcode = puzzle.hashcode;
            zero = new Point(puzzle.zero.X, puzzle.zero.Y);

            arrange = new byte[SIZE, SIZE];
            Loop((x, y) => arrange[x, y] = puzzle.arrange[x, y]);

            Movements = new List<PuzzleMovement>();
            foreach (var move in puzzle.Movements) Movements.Add((PuzzleMovement) move.Clone());           
        }


        /**
         * ## PROPERTIES ##
         **/
        public List<PuzzleMovement> Movements { get; private set; }


        /**
         * ## METHODS ##
         **/
        #region methods
        public bool Move(int index) => Move(Movements.FirstOrDefault(m => m.Index == index));

        public bool Move(PuzzleMovement movement)
        {
            if (IsNotValidMovement(movement)) return false;

            arrange[movement.To.X, movement.To.Y] = arrange[movement.From.X, movement.From.Y];
            arrange[movement.From.X, movement.From.Y] = 0;
            zero = movement.From;

            CalculateHashCode();
            CalculateMoves();

            return true;
        }
        
        public void Print()
        {
            for (int i = SIZE - 1; i >= 0; i--)
            {
                Console.WriteLine($"[{arrange[i, 0]}] [{arrange[i, 1]}] [{arrange[i, 2]}]");
            }

            Console.WriteLine();
            Console.WriteLine("Moves:");


            for (int i = 0; i < Movements.Count; i++)
            {
                var move = Movements[i];
                Console.WriteLine($"{i}: [{move.Index}] <- [{move.From.X}, {move.From.Y}] ");
            }
        }

        public override int GetHashCode() => hashcode;

        public override bool Equals(object obj) => obj is Puzzle puzzle && Equals(puzzle);

        public bool Equals(Puzzle puzzle)
        {
            if (HashNotEqual(puzzle.hashcode)) return false;
            if (DimensionsNotEquals(puzzle.arrange)) return false;
            return Equals(puzzle.arrange);
        }
        #endregion


        /**
         * ## PRIVATE METHODS ##
         **/
        #region private
        private void Reset(byte[,] start)
        {
            bool isDefault = start == null;
            if (isDefault)
            {
                Loop((x, y) => SetValue(x, y, GetIndex(x, y)));
            }
            else
            {
                Loop((x, y) => SetValue(x, y, start[x, y]));
            }           
            CalculateHashCode();
            CalculateMoves();
        }

        private bool Equals(byte[,] arrange)
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    if (this.arrange[x, y] != arrange[x, y]) return false;
                }
            }
            return true;
        }

        private bool HashNotEqual(int hashcode) => this.hashcode != hashcode;

        private bool DimensionsNotEquals(byte[,] arrange) => this.arrange.GetLength(0) != arrange.GetLength(0) || this.arrange.GetLength(1) != arrange.GetLength(1);

        private bool IsNotValidMovement(PuzzleMovement move) => move == null || !IsValid(move.From) || !IsValid(move.To) || move.ManhattanDistance > 1 || !zero.Equals(move.To);

        private void CalculateMoves()
        {
            Movements = new List<PuzzleMovement>();
            AddMovement(new Point(zero.X, zero.Y + 1));
            AddMovement(new Point(zero.X, zero.Y - 1));
            AddMovement(new Point(zero.X + 1, zero.Y));
            AddMovement(new Point(zero.X - 1, zero.Y));
        }

        private void AddMovement(Point from)
        {
            if (IsValid(from)) Movements.Add(new PuzzleMovement() { From = from, To = zero, Index = arrange[from.X, from.Y] });
        }

        private static bool IsValid(Point point) => point.X.Between(0, MAX) && point.Y.Between(0, MAX);

        private void SetValue(int x, int y, int value) {
            arrange[x, y] = (byte) value;
            if (value == 0) zero = new Point() { X = x, Y = y };
        }

        private void CalculateHashCode()
        {
            ClearHash();
            CalculateHash(Normalize());
        }

        private int[] Normalize()
        {
            int[] normalized = new int[AREA];
            Loop((x, y) => NormalizeNumberToCalculateHash(x, y, normalized));
            return normalized;
        }

        private static int GetIndex(int x, int y) => x * SIZE + y;

        private void CalculateHash(int[] normalized)
        {
            for (int i = 0; i < AREA; i++)
            {
                var factor = AREA - i - 1;
                hashcode += normalized[i] * (int)Numbers.GetFactorial(factor);
            }
        }

        private bool NormalizeNumberToCalculateHash(int x, int y, int[] normalized)
        {
            var value = arrange[x, y];
            var index = GetIndex(x, y);
            normalized[index] = value - index;
            for (int j = index - 1; j >= 0; j--)
            {
                if (normalized[j] > value) normalized[index]++;
            }
            return true;
        }

        private void ClearHash() => hashcode = 0;

        private void Loop(PuzzleProcess exec)
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    exec(x, y);
                }
            }
        }

        public object Clone() => new Puzzle(this);
        #endregion
    }
}