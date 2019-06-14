using System;
using Genesis.Organizer;
using Genesis.Pathfinder;

namespace Genesis.SlidingPuzzle.AI
{
    public class PuzzleNode : Node<Puzzle, PuzzleRoute>
    {
        public override int PriorityValue { get; set; }
    }
}
