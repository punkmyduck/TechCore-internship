using Xunit;

namespace Tests
{
    public class FirstUnitTest
    {
        [Fact]
        public void Abs_ReturnsAbsoluteNumber_WhenNegativeNumber()
        {
            int b = -5;

            int result = Math.Abs(b);

            Assert.Equal(5, result);
        }

        [Theory]
        [InlineData(-1,1)]
        [InlineData(-5,5)]
        [InlineData(0,0)]
        [InlineData(10,10)]
        public void Abs_ReturnsAbsoluteValue_ForVariousNumbers(int input, int expected)
        {
            int result = Math.Abs(input);
            Assert.Equal(expected, result);
        }
    }
}
