using System;
using System.Collections.Generic;

namespace Genesis.Pathfinder
{
    public interface IEnvironment<T> : ICloneable
    {
        List<T> Movements { get; }
        bool Move(T move);
    }
}