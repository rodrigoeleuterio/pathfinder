using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pathfinder.Core.PuzzleGame
{
    public class Puzzle
    {
        private delegate bool PuzzleProcess(int x, int y, byte index);

        private const int SIZE = 3;
        private const int MAX = SIZE - 1;
        private const int AREA = SIZE * SIZE;

        private int hashcode = 0;

        public byte[,] Arrange { get; set; }
        public Point[] Pieces { get; set; }
        public Point Zero { get; set; }
        public List<PuzzleMove> Moves { get; private set; }

        public Puzzle()
        {
            Arrange = new byte[SIZE, SIZE];
            Pieces = new Point[AREA];
            Reset();
        }

        public void Reset()
        {
            ClearHash();
            Loop((x, y, index) =>
            {
                IndexMatrix(x, y, index);
                SetValue(x, y, index);
                return true;
            });
            CalculateHashCode();
            CalculateMoves();
        }

        public void Move(PuzzleMove move)
        {
            if (IsNotValidMovement(move)) throw new InvalidOperationException("This movement is not allowed!");

            Arrange[move.To.X, move.To.Y] = Arrange[move.From.X, move.From.Y];
            Arrange[move.From.X, move.From.Y] = 0;
            Zero = move.From;

            CalculateHashCode();
            CalculateMoves();
        }

        public override int GetHashCode() => hashcode;

        public override bool Equals(object obj)
        {
            return obj is Puzzle puzzle && Equals(puzzle);
        }

        public bool Equals(Puzzle puzzle)
        {
            var result = true;
            Loop((x, y, index) =>
            {
                if (Arrange[x, y] != puzzle.Arrange[x, y]) result = false;
                return result;
            });
            return result;
        }

        private bool IsNotValidMovement(PuzzleMove move)
        {
            return !IsValid(move.From) || !IsValid(move.To) || move.ManhattanDistance > 1 || !Zero.Equals(move.To);
        }

        private void CalculateMoves()
        {
            Moves = new List<PuzzleMove>();
            AddMovement(new Point(Zero.X, Zero.Y + 1));
            AddMovement(new Point(Zero.X, Zero.Y - 1));
            AddMovement(new Point(Zero.X + 1, Zero.Y));
            AddMovement(new Point(Zero.X - 1, Zero.Y));
        }

        private void AddMovement(Point from)
        {
            if (IsValid(from)) Moves.Add(new PuzzleMove() { From = from, To = Zero });
        }

        private static bool IsValid(Point point) => point.X.Between(0, MAX) && point.Y.Between(0, MAX);

        private void SetValue(int x, int y, byte value) {
            Arrange[x, y] = value;
            if (value == 0) Zero = new Point() { X = x, Y = y };
        }
        
        private void IndexMatrix(int x, int y, int index) => Pieces[index] = new Point() { X = x, Y = y };

        private void CalculateHashCode()
        {
            ClearHash();
            CalculateHashCode(Normalize());
        }

        private int[] Normalize()
        {
            int[] normalized = new int[AREA];
            Loop((x, y, index) => NormalizeNumber(x, y, index, normalized));
            return normalized;
        }

        private void CalculateHashCode(int[] normalized)
        {
            for (int i = 0; i < AREA; i++)
            {
                var factor = AREA - i - 1;
                hashcode += normalized[i] * (int)Numbers.GetFactorial(factor);
            }
        }

        private bool NormalizeNumber(int x, int y, byte index, int[] normalized)
        {
            var value = Arrange[x, y];
            normalized[index] = value - index;
            for (int j = index - 1; j >= 0; j--)
            {
                if (normalized[j] > value) normalized[index]++;
            }
            return true;
        }

        private void ClearHash() => hashcode = 0;

        private void Loop(PuzzleProcess process)
        {
            byte index = 0;
            for (int x = 0; x < SIZE; x++)
                for (int y = 0; y < SIZE; y++)
                    if (!process(x, y, index++)) return;
        }

        //private void IncrementHashOld(int x, int y, int index) => hashcode += Arrange[x, y] * (int)Math.Pow(AREA, index);
    }
}