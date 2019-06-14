using System;
using System.Collections.Generic;

namespace Genesis.Pathfinder
{
    public interface IRoute : ICloneable
    {
        int Cost { get; }
    }
}