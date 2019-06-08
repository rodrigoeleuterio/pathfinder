using System;
using System.Collections.Generic;

namespace Pathfinder.Core
{
    public interface IEnvironment<T> : ICloneable
    {
        List<T> Movements { get; }
        bool Move(T move);
    }
}