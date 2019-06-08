using System.Collections.Generic;

namespace Pathfinder.Core.DataStructure
{
    public class QueueOrganizer<T> : IOrganizer<T>
    {
        private readonly Queue<T> queue = new Queue<T>();

        public int Count => queue.Count;
        public bool IsEmpty => Count == 0;
        public void Clear() => queue.Clear();
        public T Draw() => queue.Dequeue();
        public void Put(T value) => queue.Enqueue(value);     
        public T ViewNext() => queue.Peek();
    }
}