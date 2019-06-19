using System;

namespace Eleutherius.Genesis.Pathfinder
{
    public interface IRoute : ICloneable
    {
        int Cost { get; }
    }
}