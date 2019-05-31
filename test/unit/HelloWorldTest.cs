using Pathfinder.Core;
using System;
using Xunit;

namespace Pathfinder.Test.Unit.HelloWorldTest
{
    public class WhenGetMessage
    {
        private const string HELLO_WORLD = "Hello World!";

        [Fact]
        public void ShouldReturnHelloWorld()
        {
            Assert.Equal(HELLO_WORLD, HelloWorld.Message);
        }
    }
    
}