using System.Collections.Generic;

namespace Pathfinder.Core.DataStructure
{
    public class StackOrganizer<T> : IOrganizer<T>
    {
        private readonly Stack<T> stack = new Stack<T>();

        public int Count => stack.Count;
        public bool IsEmpty => stack.Count == 0;
        public void Clear() => stack.Clear();
        public T Draw() => stack.Pop();
        public void Put(T value) => stack.Push(value);
        public T ViewNext() => stack.Peek();
    }
}
