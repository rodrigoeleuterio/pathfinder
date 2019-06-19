using Eleutherius.Genesis.Pathfinder;

namespace Eleutherius.Genesis.SlidingPuzzle.AI
{
    public class PuzzleNode : Node<Puzzle, PuzzleRoute>
    {
        public override int PriorityValue { get; set; }
    }
}
