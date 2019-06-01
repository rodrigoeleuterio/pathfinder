using System;

namespace Pathfinder.Core
{
    public class HelloWorld
    {
        public string CreateMessage(bool goSleep, string name) {
            var greetings = goSleep ? "Good Night" : "Good Day";
            
            return $"{greetings}! {name}";
        }
    }
}
