using Eleutherius.Genesis.Pathfinder;

namespace Eleutherius.Genesis.SlidingPuzzle.AI
{
    public class PuzzleAnalizer : Analizer<PuzzleNode, Puzzle, PuzzleRoute>
    {
        protected override int CalculateHeuristic(PuzzleNode node, Puzzle objective)
        {
            Puzzle state = node.State;

            int total = 0;
            for (int i = 0; i < Puzzle.AREA; i++) 
            {
                var route = new PuzzleRoute() 
                {
                    From = state.mapIndexed[i],
                    To = objective.mapIndexed[i]
                };

                total += route.ManhattanDistance;
            }
            
            return total;
        }

        protected override PuzzleNode CreateNodeInstance() => new PuzzleNode();
    }
}
