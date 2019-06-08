using Pathfinder.Core.DataStructure;
using System;
using Xunit;

namespace Pathfinder.Test.Core.DataStructure
{
    public class RaffleOrganizerTest
    {
        private const string FIRST = "First Element";
        private const string SECOND = "Second Element";
        private const string THIRD = "Third Element";

        protected RaffleOrganizer<string> organizer;

        public class Put : RaffleOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, FIRST)]
            public void GivenInitOrganizerWhenPutValueShouldHavePlusOneElement(string[] init, string value)
            {
                GivenInitRaffleOrganizer(init);
                organizer.Put(value);
                Assert.Equal(init.Length + 1, organizer.Count);
            }
        }

        public class Count : RaffleOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, 0)]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, 3)]
            public void GivenInitOrganizerWhenGetCountShouldReturnXElements(string[] init, int count)
            {
                GivenInitRaffleOrganizer(init);
                Assert.Equal(count, organizer.Count);
            }
        }

        public class View : RaffleOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, 3)]
            public void GivenInitOrganizerWhenViewNextValueShouldReturnRandomAddedValue(string[] init, int count)
            {
                GivenInitRaffleOrganizer(init);
                Assert.NotNull(organizer.ViewNext());
                Assert.Equal(count, organizer.Count);
            }


            [Fact]
            public void GivenOrganizerIsEmptyWhenViewNextValueShouldThrowExceptionWithOrganizerEmptyMessage()
            {
                GivenInitRaffleOrganizer(new string[] { });
                var ex = Assert.Throws<InvalidOperationException>(() => { organizer.ViewNext(); });
                Assert.Equal("Raffle empty.", ex.Message);
            }
        }

        public class IsEmpty : RaffleOrganizerTest
        {
            [Theory]
            [InlineData(new string[] {  }, true)]
            [InlineData(new string[] { FIRST }, false)]
            public void GivenInitOrganizerWhenVerifyIfOrganizerIsEmptyShouldReturnTheCorrectBoolean(string[] init, bool value)
            {
                GivenInitRaffleOrganizer(init);
                Assert.Equal(value, organizer.IsEmpty);
            }
        }

        public class Draw : RaffleOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, 2)]
            public void GivenInitOrganizerWhenDrawValueShouldReturnRandomAddedValueAndHaveMinusOneElement(string[] init, int count)
            {
                GivenInitRaffleOrganizer(init);
                Assert.NotNull(organizer.Draw());
                Assert.Equal(count, organizer.Count);
            }

            [Fact]
            public void GivenOrganizerIsEmptyWhenDrawValueShouldThrowExceptionWithOrganizerEmptyMessage()
            {
                GivenInitRaffleOrganizer(new string[] { });
                var ex = Assert.Throws<InvalidOperationException>(() => { organizer.Draw(); });
                Assert.Equal("Raffle empty.", ex.Message);
            }
        }

        public class Clear : RaffleOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, true)]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, true)]
            public void GivenInitOrganizerWhenClearTheOrganizerShouldBeEmpty(string[] init, bool empty)
            {
                GivenInitRaffleOrganizer(init);
                organizer.Clear();
                Assert.Equal(empty, organizer.IsEmpty);
            }
        }

        protected void GivenInitRaffleOrganizer(string[] init)
        {
            organizer = new RaffleOrganizer<string>();
            foreach (var item in init) organizer.Put(item);
        }
    }
}
