using Genesis.Pathfinder;

namespace Genesis.SlidingPuzzle.AI
{
    public class PuzzleAnalizer : Analizer<PuzzleNode, Puzzle, PuzzleRoute>
    {        
        protected override PuzzleNode GetNodeInstance() => new PuzzleNode();

        protected override int Heuristic(PuzzleNode node)
        {
            return node.State.CalculateDistance(Objective);
        }
    }
}
