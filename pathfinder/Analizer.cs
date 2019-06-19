using Eleutherius.Genesis.Organizer;
using System;
using System.Collections.Generic;

namespace Eleutherius.Genesis.Pathfinder
{
    public abstract class Analizer<N, T, E> where N: Node<T, E> where T: IEnvironment<E> where E: IRoute
    {
        public event Action<string> OnFinished;

        protected abstract N CreateNodeInstance();
        protected T Objective { get; set; }
        protected Func<N, T, int> Heuristic { get; set; }

        /**
         * ## METHODS ## 
         **/
        protected abstract void SetHeuristic(string name = "cost");

        public N SearchBreadth(T start, T end)
        {
            Objective = end;
            return Search(new QueueOrganizer<N>(), start, end);
        }

        public N SearchDeepth(T start, T end)
        {
            Objective = end;
            return Search(new StackOrganizer<N>(), start, end);
        }

        public N SearchByUniformCost(T start, T end)
        {
            Objective = end;
            SetHeuristic("cost");
            return Search(new AscendentOrganizer<N>(), start, end);
        }

        protected N Search(IOrganizer<N> organizer, T start, T end) {
            var timeStart = DateTime.Now;
            organizer.Put(CreateFirstNode(start));

            int nodes = 1;
            int loops = 0;
            while (!organizer.IsEmpty)
            {
                loops++;
                var node = organizer.Draw();

                if (end.Equals(node.State))
                {
                    var timeEnd = DateTime.Now;
                    OnFinished?.Invoke($"nodes: {nodes} / interactions: {loops} / deep: {node.Deep} / elapse time: {timeEnd - timeStart}");
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
                if (state.Move(movement) && node.IsNotMovementInLoop(state))
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

