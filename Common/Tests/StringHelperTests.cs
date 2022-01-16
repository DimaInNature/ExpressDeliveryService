using System.Linq;
using Xunit;

namespace Common.Tests
{
    public class StringHelperTests
    {
        [Theory]
        [InlineData("First")]
        [InlineData("Sec_Ond")]
        [InlineData("Sec Ond")]
        [InlineData(" Thir d")]
        public void StrIsNotNullOrWhiteSpace_Test_True(string value)
        {
            Assert.True(StrIsNotNullOrWhiteSpace(value));
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("     ")]
        public void StrIsNotNullOrWhiteSpace_Test_False(string value)
        {
            Assert.False(StrIsNotNullOrWhiteSpace(value));
        }

        private bool StrIsNotNullOrWhiteSpace(params string[] stringsArray) =>
           stringsArray.All(stroke => !string.IsNullOrWhiteSpace(stroke));
    }
}
