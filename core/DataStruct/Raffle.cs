using System;
using System.Collections.Generic;

namespace Pathfinder.Core.DataStruct
{
    public class Raffle<T> : IOrganizer<T>
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
            if (IsEmpty) throw new InvalidOperationException();
            return list[last];
        }

        private void RemoveLast() => list.RemoveAt(last);

        private void CalculateLastIndex() => last = Count - 1;

        /// <summary>
        /// Sorteia um index da lista e troca o valor com a ultima posição.
        /// É mais rápido remover o último elemento de uma lista, pois não há reindexização dos elementos.
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
