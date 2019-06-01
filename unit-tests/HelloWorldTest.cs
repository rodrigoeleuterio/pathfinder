using Pathfinder.Core;
using System;
using Xunit;

namespace Pathfinder.UnitTests
{
    public class HelloWorldTest
    {
        private readonly HelloWorld helloWorld = new HelloWorld();

        public class WhenCreateMessage : HelloWorldTest
        {
            private const string GOOD_NIGHT = "Good Night! Rodrigo";

            [Fact]
            public void GivenRodrigoIsGoingToSleepShouldReturnGoodNightMessage()
            {
                Assert.Equal(GOOD_NIGHT, helloWorld.CreateMessage(true, "Rodrigo"));
            }
        }
    }
}