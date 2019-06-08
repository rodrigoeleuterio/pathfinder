using System;
using System.Drawing;

namespace PuzzleGame
{
    public class PuzzleMovement : ICloneable
    {
        public PuzzleMovement() { }

        private PuzzleMovement(PuzzleMovement puzzle)
        {
            From = new Point(puzzle.From.X, puzzle.From.Y);
            To = new Point(puzzle.To.X, puzzle.To.Y);
            Index = puzzle.Index;
        }


        /**
         * ## PROPERTIES ##
         **/
        public Point From { get; set; }
        public Point To { get; set; }
        public int Index { get; set; }


        /**
         * ## METHODS ##
         **/
        public int ManhattanDistance { get => Math.Abs(To.X - From.X) + Math.Abs(To.Y - From.Y); }

        public object Clone() => new PuzzleMovement(this);
    }
}
