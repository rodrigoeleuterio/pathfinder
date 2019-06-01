namespace Pathfinder.Core.DataStruct
{
    public class Queue<T> : IOrganizer<T>
    {
        private readonly System.Collections.Generic.Queue<T> queue = new System.Collections.Generic.Queue<T>();

        public int Count => queue.Count;
        public bool IsEmpty => Count == 0;
        public void Clear() => queue.Clear();
        public T Draw() => queue.Dequeue();
        public void Put(T value) => queue.Clear();// queue.Enqueue(value);     
        public T ViewNext() => queue.Peek();
    }
}