using System;
using System.Collections.Generic;

namespace Pathfinder.Core.DataStructure
{
    public class RaffleOrganizer<T> : IOrganizer<T>
    {
        private readonly Random rnd = new Random();
        private readonly IList<T> list = new List<T>();
        private int last = -1;

        public int Count => list.Count;

        public bool IsEmpty => Count == 0;

        public void Clear()
        {
            list.Clear();
            last = -1;
        }

        public T Draw()
        {
            var value = ViewNext();
            RemoveLast();
            CalculateLastIndex();
            Sort();
            return value;
        }

        public void Put(T value)
        {
            list.Add(value);
            CalculateLastIndex();
            Sort();
        }

        public T ViewNext()
        {
            if (IsEmpty) throw new InvalidOperationException("Raffle empty.");
            return list[last];
        }

        private void RemoveLast() => list.RemoveAt(last);

        private void CalculateLastIndex() => last = Count - 1;

        /// <summary>
        /// Sort a index and change the index value with the last position value.
        /// It´s fastier to remove the last element of list.
        /// </summary>
        private void Sort()
        {
            var next = rnd.Next(Count);
            var value = list[last];
            list[last] = list[next];
            list[next] = value;
        }
    }
}
