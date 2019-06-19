using System;
using Xunit;

namespace Eleutherius.Genesis.Organizer.Xunit
{
    public class QueueOrganizerTest
    {
        private const string FIRST = "First Element";
        private const string SECOND = "Second Element";
        private const string THIRD = "Third Element";

        protected QueueOrganizer<string> organizer;

        public class Put : QueueOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, FIRST)]
            public void GivenInitOrganizerWhenPutValueShouldHavePlusOneElement(string[] init, string value)
            {
                GivenInitQueueOrganizer(init);
                organizer.Put(value);
                Assert.Equal(init.Length + 1, organizer.Count);
            }
        }

        public class Count : QueueOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, 0)]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, 3)]
            public void GivenInitOrganizerWhenGetCountShouldReturnXElements(string[] init, int count)
            {
                GivenInitQueueOrganizer(init);
                Assert.Equal(count, organizer.Count);
            }
        }

        public class View : QueueOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, FIRST)]
            public void GivenInitOrganizerWhenViewNextValueShouldReturnFirstAddedValue(string[] init, string first)
            {
                GivenInitQueueOrganizer(init);
                Assert.Equal(first, organizer.ViewNext());
                Assert.Equal(init.Length, organizer.Count);
            }

            [Fact]
            public void GivenOrganizerIsEmptyWhenViewNextValueShouldThrowExceptionWithOrganizerEmptyMessage()
            {
                GivenInitQueueOrganizer(new string[] { });
                var ex = Assert.Throws<InvalidOperationException>(() => { organizer.ViewNext(); });
                Assert.Equal("Queue empty.", ex.Message);
            }
        }

        public class IsEmpty : QueueOrganizerTest
        {
            [Theory]
            [InlineData(new string[] {  }, true)]
            [InlineData(new string[] { FIRST }, false)]
            public void GivenInitOrganizerWhenVerifyIfOrganizerIsEmptyShouldReturnTheCorrectBoolean(string[] init, bool value)
            {
                GivenInitQueueOrganizer(init);
                Assert.Equal(value, organizer.IsEmpty);
            }
        }

        public class Draw : QueueOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, FIRST)]
            public void GivenInitOrganizerWhenDrawValueShouldReturnFirstAddedValueAndHaveMinusOneElement(string[] init, string expected)
            {
                GivenInitQueueOrganizer(init);
                var value = organizer.Draw();
                Assert.Equal(expected, value);
                Assert.Equal(init.Length - 1, organizer.Count);
            }

            [Fact]
            public void GivenOrganizerIsEmptyWhenDrawValueShouldThrowExceptionWithOrganizerEmptyMessage()
            {
                GivenInitQueueOrganizer(new string[] { });
                var ex = Assert.Throws<InvalidOperationException>(() => { organizer.Draw(); });
                Assert.Equal("Queue empty.", ex.Message);
            }
        }

        public class Clear : QueueOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, true)]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, true)]
            public void GivenInitOrganizerWhenClearTheOrganizerShouldBeEmpty(string[] init, bool empty)
            {
                GivenInitQueueOrganizer(init);
                organizer.Clear();
                Assert.Equal(empty, organizer.IsEmpty);
            }
        }

        protected void GivenInitQueueOrganizer(string[] init)
        {
            organizer = new QueueOrganizer<string>();
            foreach (var item in init) organizer.Put(item);
        }
    }
}
