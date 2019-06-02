using Pathfinder.Core.DataStructure;
using System;
using Xunit;

namespace Pathfinder.Test.Core.DataStructure
{
    public class StackOrganizerTest
    {
        private const string FIRST = "First Element";
        private const string SECOND = "Second Element";
        private const string THIRD = "Third Element";

        protected StackOrganizer<string> organizer;

        public class Put : StackOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, FIRST)]
            public void GivenInitOrganizerWhenPutValueShouldHavePlusOneElement(string[] init, string value)
            {
                GivenInitStackOrganizer(init);
                organizer.Put(value);
                Assert.Equal(init.Length + 1, organizer.Count);
            }
        }

        public class Count : StackOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, 0)]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, 3)]
            public void GivenInitOrganizerWhenGetCountShouldReturnXElements(string[] init, int count)
            {
                GivenInitStackOrganizer(init);
                Assert.Equal(count, organizer.Count);
            }
        }

        public class View : StackOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, THIRD)]
            public void GivenInitOrganizerWhenViewNextValueShouldReturnLastAddedValue(string[] init, string first)
            {
                GivenInitStackOrganizer(init);
                Assert.Equal(first, organizer.ViewNext());
                Assert.Equal(init.Length, organizer.Count);
            }

            [Fact]
            public void GivenOrganizerIsEmptyWhenViewNextValueShouldThrowExceptionWithOrganizerEmptyMessage()
            {
                GivenInitStackOrganizer(new string[] { });
                var ex = Assert.Throws<InvalidOperationException>(() => { organizer.ViewNext(); });
                Assert.Equal("Stack empty.", ex.Message);
            }
        }

        public class IsEmpty : StackOrganizerTest
        {
            [Theory]
            [InlineData(new string[] {  }, true)]
            [InlineData(new string[] { FIRST }, false)]
            public void GivenInitOrganizerWhenVerifyIfOrganizerIsEmptyShouldReturnTheCorrectBoolean(string[] init, bool value)
            {
                GivenInitStackOrganizer(init);
                Assert.Equal(value, organizer.IsEmpty);
            }
        }

        public class Draw : StackOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, THIRD)]
            public void GivenInitOrganizerWhenDrawValueShouldReturnLastAddedValueAndHaveMinusOneElement(string[] init, string expected)
            {
                GivenInitStackOrganizer(init);
                var value = organizer.Draw();
                Assert.Equal(expected, value);
                Assert.Equal(init.Length - 1, organizer.Count);
            }

            [Fact]
            public void GivenOrganizerIsEmptyWhenDrawValueShouldThrowExceptionWithOrganizerEmptyMessage()
            {
                GivenInitStackOrganizer(new string[] { });
                var ex = Assert.Throws<InvalidOperationException>(() => { organizer.Draw(); });
                Assert.Equal("Stack empty.", ex.Message);
            }
        }

        public class Clear : StackOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, true)]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, true)]
            public void GivenInitOrganizerWhenClearTheOrganizerShouldBeEmpty(string[] init, bool empty)
            {
                GivenInitStackOrganizer(init);
                organizer.Clear();
                Assert.Equal(empty, organizer.IsEmpty);
            }
        }

        protected void GivenInitStackOrganizer(string[] init)
        {
            organizer = new StackOrganizer<string>();
            foreach (var item in init) organizer.Put(item);
        }
    }
}
