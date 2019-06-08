using Pathfinder.Core;

namespace PuzzleGame
{
    public class PuzzleSolver : Solver<PuzzleNode, Puzzle, PuzzleMovement>
    {
        protected override PuzzleNode GetNodeInstance()
        {
            return new PuzzleNode();
        }
    }
}
