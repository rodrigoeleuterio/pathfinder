using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pathfinder.Core.PuzzleGame
{
    delegate void Piece(int x, int y, byte index);

    public class Puzzle
    {
        private const int SIZE = 3;
        private const int AREA = SIZE * SIZE;

        private int hashcode = 0;

        public byte[,] Arrange { get; set; }
        public Point[] Pieces { get; set; }
        public Point Zero { get; set; }
        public List<Point> Moves { get; private set; }

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
                IncrementHash(x, y, index);
                SetValue(x, y, index);
                SetZero(x, y, index);
            });
            CalculateMoves();
        }

        public void Move(Point piece)
        {
            if (!IsInnerRange(piece)) return;
        }

        public override int GetHashCode() => hashcode;


        private void CalculateMoves()
        {
            Moves = new List<Point>();
            AddMovement(new Point(Zero.X, Zero.Y + 1));
            AddMovement(new Point(Zero.X, Zero.Y - 1));
            AddMovement(new Point(Zero.X + 1, Zero.Y));
            AddMovement(new Point(Zero.X - 1, Zero.Y));
        }

        private void AddMovement(Point north)
        {
            if (IsInnerRange(north)) Moves.Add(north);
        }

        private static bool IsInnerRange(int value) => value >= 0 && value < SIZE;

        private static bool IsInnerRange(Point point) => IsInnerRange(point.X) && IsInnerRange(point.Y);

        private void SetValue(int x, int y, byte value) => Arrange[x, y] = value;

        private void SetZero(int x, int y, byte value)
        {
            if (value == 0) Zero = new Point() { X = x, Y = y };
        }

        private void IndexMatrix(int x, int y, int index) => Pieces[index] = new Point() { X = x, Y = y };

        private void CalculateHashCode()
        {
            ClearHash();
            Loop((x, y, index) => IncrementHash(x, y, index));
        }

        private void IncrementHash(int x, int y, int index) => hashcode += Arrange[x, y] * (int)Math.Pow(AREA, index);

        private void ClearHash() => hashcode = 0;


        private void Loop(Piece piece)
        {
            byte index = 0;
            for (int x = 0; x < SIZE; x++)
                for (int y = 0; y < SIZE; y++)
                    piece(x, y, index++);
        }
    }
}
