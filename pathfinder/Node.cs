using Eleutherius.Genesis.Organizer;

namespace Eleutherius.Genesis.Pathfinder
{
    public abstract class Node<T, E> : IPriority where T : IEnvironment<E>
    {
        /**
         * ## PROPERTIES ## 
         **/
        public int Deep { get; set; }
        public int Cost { get; set; }
        public T State { get; set; }
        public Node<T, E> Parent { get; set; }
        public E Movement { get; set; }
        public abstract int PriorityValue { get; set; }

        public bool IsNotMovementInLoop(T state)
        {
            var parent = this;
            while (parent != null)
            {
                if (parent.State.Equals(state))
                {
                    return false;
                }
                parent = parent.Parent;
            }
            return true;
        }
    }
}
