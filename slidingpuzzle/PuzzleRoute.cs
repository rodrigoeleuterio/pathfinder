using System;
using System.Drawing;
using Eleutherius.Genesis.Pathfinder;

namespace Eleutherius.Genesis.SlidingPuzzle
{
    public class PuzzleRoute : IRoute
    {
        public PuzzleRoute() { }

        private PuzzleRoute(PuzzleRoute puzzle)
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

        public int Cost => 1;

        public object Clone() => new PuzzleRoute(this);
    }
}
