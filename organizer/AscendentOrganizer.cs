using System;
using System.Collections.Generic;

namespace Pathfinder.Core.DataStructure
{
    public class AscendentOrganizer<T> : IOrganizer<T> where T: IPriority
    {
        private readonly SortedDictionary<int, IOrganizer<T>> basket = new SortedDictionary<int, IOrganizer<T>>();
        private int next = int.MaxValue;
        
        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;

        public void Clear()
        {
            next = int.MaxValue;
            Count = 0;
            basket.Clear();
        }

        public T Draw()
        {
            if (IsEmpty) throw new InvalidOperationException("Organizer is empty.");
            var value = NextBasket.Draw();
            RemoveItem();
            DecrementCount();
            return value;
        }

        public void Put(T value)
        {
            AddItem(value, value.PriorityValue);
            IncrementCount();
        }

        public T ViewNext()
        {
            if (IsEmpty) throw new InvalidOperationException("Organizer is empty.");
            return NextBasket.ViewNext();
        }

        private IOrganizer<T> NextBasket { get => basket[next]; }

        private void AddItem(T value, int key)
        {
            if (basket.ContainsKey(key))
            {
                basket[key].Put(value);
            }
            else
            {
                basket.Add(key, CreateQueue(value));
            }

            FindNextIndex(key);
        }

        private void RemoveItem()
        {
            if (NextQueueIsNotEmpty) return;

            basket.Remove(next);
            FindNextIndex();
        }        
        
        private static QueueOrganizer<T> CreateQueue(T value)
        {
            var queue = new QueueOrganizer<T>();
            queue.Put(value);
            return queue;
        }

        private bool NextQueueIsNotEmpty { get => !NextBasket.IsEmpty; }

        private void IncrementCount() => Count += 1;

        private void DecrementCount() => Count -= 1;

        private void FindNextIndex() => next = basket.Keys.GetEnumerator().Current;

        private void FindNextIndex(int value) => next = Math.Min(next, value);
    }
}
