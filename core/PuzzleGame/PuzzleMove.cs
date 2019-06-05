using System;
using System.Drawing;

namespace Pathfinder.Core.PuzzleGame
{
    public class PuzzleMove
    {
        public Point From { get; set; }
        public Point To { get; set; }

        public int ManhattanDistance { get => Math.Abs(To.X - From.X) + Math.Abs(To.Y - From.Y); }
    }
}
