using Genesis.Pathfinder;

namespace Genesis.SlidingPuzzle.AI
{
    public class PuzzleAnalizer : Analizer<PuzzleNode, Puzzle, PuzzleRoute>
    {
        protected override PuzzleNode GetNodeInstance() => new PuzzleNode();
    }
}
