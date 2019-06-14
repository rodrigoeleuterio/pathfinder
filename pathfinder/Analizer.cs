using Genesis.Organizer;
using System.Collections.Generic;

namespace Genesis.Pathfinder
{
    public abstract class Analizer<N, T, E> where N: Node<T, E> where T: IEnvironment<E> where E: IRoute
    {
        protected abstract N GetNodeInstance();
        protected abstract int Heuristic(N node);
        protected T Objective { get; private set; }

        /**
         * ## METHODS ## 
         **/
        public N BreadthSearch(T start, T end)
        {
            Objective = end;
            return Search(new QueueOrganizer<N>(), start, end);
        }

        public N DepthSearch(T start, T end)
        {
            Objective = end;
            return Search(new StackOrganizer<N>(), start, end);
        }

        public N UniformCostSearch(T start, T end)
        {
            Objective = end;
            return Search(new AscendentOrganizer<N>(), start, end);
        }

        private N Search(IOrganizer<N> organizer, T start, T end) {
            organizer.Put(CreateFirstNode(start));

            int i = 0;
            while (!organizer.IsEmpty)
            {
                var node = organizer.Draw();

                if (end.Equals(node.State)) return node;
                
                foreach (var newNode in Expand(node))
                {
                    System.Console.Clear();
                    System.Console.WriteLine($"{++i} / {node.Deep}");
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
            N node = GetNodeInstance();
            node.State = start;
            return node;
        }

        private N CreateNode(N parent, E route, T state)
        {
            N node = GetNodeInstance();

            node.Deep = parent.Deep + 1;
            node.Cost = route.Cost;
            node.State = state;
            node.Parent = parent;
            node.Movement = route;
            node.PriorityValue = Heuristic(node);

            return node;
        }
    }
}
