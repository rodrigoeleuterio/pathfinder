using Genesis.Organizer;
using Genesis.Pathfinder;
using System;
using System.Collections.Generic;

namespace Genesis.SlidingPuzzle.AI
{
    public class PuzzleAnalizer : Analizer<PuzzleNode, Puzzle, PuzzleRoute>
    {
        private static readonly Dictionary<string, Func<PuzzleNode, Puzzle, int>> heuristics;

        static PuzzleAnalizer()
        {
            heuristics = new Dictionary<string, Func<PuzzleNode, Puzzle, int>>()
            {
                ["cost"] = (node, objective) => node.Cost,
                ["greedy"] = (node, objective) => node.State.CalculateDistance(objective),
                ["a*"] = (node, objective) => node.State.CalculateDistance(objective) + node.Cost
            };
        }

        public PuzzleNode SearchGreedy(Puzzle start, Puzzle end)
        {
            Objective = end;
            SetHeuristic("greedy");
            return Search(new AscendentOrganizer<PuzzleNode>(), start, end);
        }

        public PuzzleNode SearchByHeuristic(Puzzle start, Puzzle end)
        {
            Objective = end;
            SetHeuristic("a*");
            return Search(new AscendentOrganizer<PuzzleNode>(), start, end);
        }

        protected override void SetHeuristic(string name = "cost")
        {
            Heuristic = heuristics[name];
        }

        protected override PuzzleNode CreateNodeInstance() => new PuzzleNode();
    }
}
