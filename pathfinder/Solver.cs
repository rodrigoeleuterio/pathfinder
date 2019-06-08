using Pathfinder.Core.DataStructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pathfinder.Core
{
    public abstract class Solver<N, T, E> where N: Node<T, E> where T: IEnvironment<E>
    {
        protected abstract N GetNodeInstance();
        

        /**
         * ## METHODS ## 
         **/
        public N SearchByExtension(T start, T end)
        {
            IOrganizer<N> organizer = new QueueOrganizer<N>();

            organizer.Put(CreateFirstNode(start));

            while (!organizer.IsEmpty)
            {
                var node = organizer.Draw();

                if (end.Equals(node.State)) return node;
                
                foreach (var newNode in Expand(node))
                {
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
            foreach (var movement in node.State.Movements)
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

        private N CreateNode(N parent, E move, T state)
        {
            N node = GetNodeInstance();

            node.Deep = parent.Deep + 1;
            node.Cost = parent.Cost;
            node.State = state;
            node.Parent = parent;
            node.Movement = move;

            return node;
        }
    }
}
