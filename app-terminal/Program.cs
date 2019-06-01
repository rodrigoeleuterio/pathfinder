using Pathfinder.Core;
using System;

namespace Pathfinder.App.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new HelloWorld().CreateMessage(false, "Rodrigo"));
        }
    }
}
