using Pathfinder.Core.DataStruct;
using System;
using System.Text;
using Xunit;

namespace Pathfinder.Test.Core.DataStruct
{
    public class QueueTest
    {
        private const string FIRST = "First Element";
        private const string SECOND = "Second Element";
        private const string THIRD = "Third Element";

        protected Queue<string> queue;

        public class Put : QueueTest
        {
            [Theory]
            [InlineData(new string[] { }, FIRST)]
            public void GivenInitQueueWhenPutValueShouldHavePlusOneElement(string[] init, string value)
            {
                GivenInitQueue(init);
                queue.Put(value);
                Assert.Equal(init.Length + 1, queue.Count);
            }
        }

        public class Count : QueueTest
        {
            [Theory]
            [InlineData(new string[] { }, 0)]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, 3)]
            public void GivenInitQueueWhenGetCountShouldReturnXElements(string[] init, int count)
            {
                GivenInitQueue(init);
                Assert.Equal(count, queue.Count);
            }
        }

        public class View : QueueTest
        {
            [Theory]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, FIRST)]
            public void GivenInitQueueWhenViewNextValueShouldReturnFirstAddedValue(string[] init, string first)
            {
                GivenInitQueue(init);
                Assert.Equal(first, queue.ViewNext());
            }
        }

        public class IsEmpty : QueueTest
        {
            [Theory]
            [InlineData(new string[] {  }, true)]
            [InlineData(new string[] { FIRST }, false)]
            public void GivenInitQueueWhenVerifyIfQueueIsEmptyShouldReturnTheCorrectBoolean(string[] init, bool value)
            {
                GivenInitQueue(init);
                Assert.Equal(value, queue.IsEmpty);
            }
        }

        public class Draw : QueueTest
        {
            [Theory]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, FIRST)]
            public void GivenInitQueueWhenDrawValueShouldReturnFirstAddedValueAndHaveMinusOneElement(string[] init, string expected)
            {
                GivenInitQueue(init);
                var value = queue.Draw();
                Assert.Equal(expected, value);
                Assert.Equal(init.Length - 1, queue.Count);
            }

            [Fact]
            public void GivenQueueIsEmptyWhenDrawValueShouldThrowExceptionWithQueueEmptyMessage()
            {
                GivenInitQueue(new string[] { });
                var ex = Assert.Throws<InvalidOperationException>(() => { queue.Draw(); });
                Assert.Equal("Queue empty.", ex.Message);
            }
        }

        public class Clear : QueueTest
        {
            [Theory]
            [InlineData(new string[] { }, true)]
            [InlineData(new string[] { FIRST, SECOND, THIRD }, true)]
            public void GivenInitQueueWhenClearTheQueueShouldBeEmpty(string[] init, bool empty)
            {
                GivenInitQueue(init);
                queue.Clear();
                Assert.Equal(empty, queue.IsEmpty);
            }
        }

        protected void GivenInitQueue(string[] init)
        {
            queue = new Queue<string>();
            foreach (var item in init) queue.Put(item);
        }
    }
}
