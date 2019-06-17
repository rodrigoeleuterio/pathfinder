using Genesis.Organizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Genesis.Pathfinder
{
    public abstract class Analizer<N, T, E> where N: Node<T, E> where T: IEnvironment<E> where E: IRoute
    {
        public event Action<string> OnLocateTarget;

        private List<T> graph = new List<T>();
        private bool graphMode  = false;
        protected abstract N CreateNodeInstance();
        protected T Objective { get; set; }
        protected Func<N, T, int> Heuristic { get; set; }

        /**
         * ## METHODS ## 
         **/
        public N SearchBreadth(T start, T end, bool graphMode = false)
        {
            return Search(new QueueOrganizer<N>(), start, end, graphMode);
        }

        public N SearchDeepth(T start, T end, bool graphMode = false)
        {
            return Search(new StackOrganizer<N>(), start, end, graphMode);
        }

        public N SearchByUniformCost(T start, T end, bool graphMode = false)
        {
            Heuristic = (node, objective) => node.Cost;
            return Search(new AscendentOrganizer<N>(), start, end, graphMode);
        }

        public N SearchGreedy(T start, T end, bool graphMode = false)
        {
            Heuristic = (node, objective) => CalculateHeuristic(node, objective);
            return Search(new AscendentOrganizer<N>(), start, end, graphMode);
        }

        public N SearchAsterisk(T start, T end, bool graphMode = false)
        {
            Heuristic = (node, objective) => CalculateHeuristic(node, objective) + node.Cost;
            return Search(new AscendentOrganizer<N>(), start, end, graphMode);
        }

        protected abstract int CalculateHeuristic(N node, T objective);

        protected N Search(IOrganizer<N> organizer, T start, T end, bool graphMode) {
            this.graphMode = graphMode;
            Objective = end;           

            var timeStart = DateTime.Now;
            organizer.Put(CreateFirstNode(start));

            int nodes = 1;
            int loops = 0;
            while (!organizer.IsEmpty)
            {
                loops++;
                var node = organizer.Draw();
                if (graphMode) graph.Add(node.State);

                if (end.Equals(node.State))
                {
                    var timeEnd = DateTime.Now;
                    OnLocateTarget?.Invoke($"nodes: {nodes} / interactions: {loops} / deep: {node.Deep} / elapse time: {timeEnd - timeStart}");
                    return node;
                }
                
                foreach (var newNode in Expand(node))
                {
                    nodes++;
                    organizer.Put((N)newNode);
                }     
            }
            return null;
        }

        /**
         * ## PRIVATE METHODS ## 
         **/
        private List<N> Expand(N node)
        {
            var list = new List<N>();
            foreach (var movement in node.State.Routes)
            {
                var state = (T)node.State.Clone();
                if (state.Move(movement) && (graphMode) ? !graph.Exists(m => m.Equals(state)) : node.IsNotMovementInLoop(state))
                {
                    AddNewNodeInList(list, node, movement, state);
                }
            }
            return list;
        }

        private void AddNewNodeInList(List<N> list, N parent, E move, T state)
        {
            list.Add(CreateNode(parent, move, state));
        }

        private N CreateFirstNode(T start)
        {
            N node = CreateNodeInstance();
            node.State = start;
            return node;
        }

        private N CreateNode(N parent, E route, T state)
        {
            N node = CreateNodeInstance();

            node.Deep = parent.Deep + 1;
            node.Cost = route.Cost + parent.Cost;
            node.State = state;
            node.Parent = parent;
            node.Movement = route;
            node.PriorityValue = Heuristic?.Invoke(node, Objective) ?? 0;

            return node;
        }
    }
}

