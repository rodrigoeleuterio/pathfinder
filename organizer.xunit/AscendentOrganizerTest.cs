using System;
using Xunit;

namespace Genesis.Organizer.Xunit
{
    public class AscendentOrganizerTest
    {
        private const string FIRST = "First Element:20";
        private const string SECOND = "Second Element:10";
        private const string THIRD = "Third Element:20";
        private const string FORTH = "Fourth Element:25";

        protected AscendentOrganizer<PriorityObject<string>> organizer;

        public class Put : AscendentOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, FIRST)]
            public void GivenInitOrganizerWhenPutValueShouldHavePlusOneElement(string[] init, string value)
            {
                GivenInitAscendentOrganizer(init);
                organizer.Put(CreatePriorityObject(value));
                Assert.Equal(init.Length + 1, organizer.Count);
            }
        }

        public class Count : AscendentOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, 0)]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, 3)]
            public void GivenInitOrganizerWhenGetCountShouldReturnXElements(string[] init, int count)
            {
                GivenInitAscendentOrganizer(init);
                Assert.Equal(count, organizer.Count);
            }
        }

        public class View : AscendentOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, SECOND)]
            [InlineData(new string[] { FIRST, FORTH, THIRD }, FIRST)]
            public void GivenInitOrganizerWhenViewNextValueShouldReturnLeastPriorityValue(string[] init, string first)
            {
                GivenInitAscendentOrganizer(init);
                Assert.Equal(CreatePriorityObject(first).Value, organizer.ViewNext().Value);
                Assert.Equal(init.Length, organizer.Count);
            }

            [Fact]
            public void GivenOrganizerIsEmptyWhenViewNextValueShouldThrowExceptionWithQueueEmptyMessage()
            {
                GivenInitAscendentOrganizer(new string[] { });
                var ex = Assert.Throws<InvalidOperationException>(() => { organizer.ViewNext(); });
                Assert.Equal("Organizer is empty.", ex.Message);
            }
        }

        public class IsEmpty : AscendentOrganizerTest
        {
            [Theory]
            [InlineData(new string[] {  }, true)]
            [InlineData(new string[] { FIRST }, false)]
            public void GivenInitOrganizerWhenVerifyIfOrganizerIsEmptyShouldReturnTheCorrectBoolean(string[] init, bool value)
            {
                GivenInitAscendentOrganizer(init);
                Assert.Equal(value, organizer.IsEmpty);
            }
        }

        public class Draw : AscendentOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, SECOND)]
            [InlineData(new string[] { FIRST, FORTH, THIRD }, FIRST)]
            public void GivenInitOrganizerWhenDrawValueShouldReturnLeastPriorityValueAndHaveMinusOneElement(string[] init, string expected)
            {
                GivenInitAscendentOrganizer(init);
                var drawed = organizer.Draw();
                Assert.Equal(CreatePriorityObject(expected).Value, drawed.Value);
                Assert.Equal(init.Length - 1, organizer.Count);
            }

            [Fact]
            public void GivenOrganizerIsEmptyWhenDrawValueShouldThrowExceptionWithOrganizerEmptyMessage()
            {
                GivenInitAscendentOrganizer(new string[] { });
                var ex = Assert.Throws<InvalidOperationException>(() => { organizer.Draw(); });
                Assert.Equal("Organizer is empty.", ex.Message);
            }
        }

        public class Clear : AscendentOrganizerTest
        {
            [Theory]
            [InlineData(new string[] { }, true)]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, true)]
            public void GivenInitOrganizerWhenClearTheOrganizerShouldBeEmpty(string[] init, bool empty)
            {
                GivenInitAscendentOrganizer(init);
                organizer.Clear();
                Assert.Equal(empty, organizer.IsEmpty);
            }
        }

        protected void GivenInitAscendentOrganizer(string[] init)
        {
            organizer = new AscendentOrganizer<PriorityObject<string>>();
            foreach (var item in init)
            {
                organizer.Put(CreatePriorityObject(item));
            }
        }

        private static PriorityObject<string> CreatePriorityObject(string item)
        {
            var splitted = item.Split(":");
            return new PriorityObject<string>()
            {
                Value = splitted[0],
                PriorityValue = int.Parse(splitted[1])
            };
        }
    }
}
