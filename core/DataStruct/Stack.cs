namespace Pathfinder.Core.DataStruct
{
    public class Stack<T> : IOrganizer<T>
    {
        private readonly System.Collections.Generic.Stack<T> stack = new System.Collections.Generic.Stack<T>();

        public int Count => stack.Count;
        public bool IsEmpty => stack.Count == 0;
        public void Clear() => stack.Clear();
        public T Draw() => stack.Pop();
        public void Put(T value) => stack.Push(value);
        public T ViewNext() => stack.Peek();
    }
}
