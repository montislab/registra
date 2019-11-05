using FluentAssertions;
using Xunit;

namespace RegistraWebApiTests
{
    public class Example
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact(Skip = "Fail for example")]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        [Fact]
        public void PassingTestFluent()
        {
            Add(2, 2).Should().Be(4);
        }

        [Fact(Skip = "Fail for example")]
        public void FailingTestFluent()
        {
            Add(2, 2).Should().Be(5);
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}
