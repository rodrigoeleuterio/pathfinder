using System;
using System.Collections.Generic;

namespace Eleutherius.Genesis.Pathfinder
{
    public interface IEnvironment<T> : ICloneable
    {
        List<T> Routes { get; }
        bool Move(T route);
    }
}