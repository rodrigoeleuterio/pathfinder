using System;
using System.Collections.Generic;

namespace Genesis.Pathfinder
{
    public interface IEnvironment<T> : ICloneable
    {
        List<T> Routes { get; }
        bool Move(T route);
    }
}