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
    }
}
