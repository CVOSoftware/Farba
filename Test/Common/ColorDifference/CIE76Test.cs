using System.Windows.Media;
using Farba.Common.ColorDifference;
using Xunit;

namespace Test.Common.ColorDifference
{
    public class CIE76Test
    {
        #region PositiveTest

        [Fact]
        public void CalculateTheDifference_PositiveTest_69_74_79_and_174_179_174()
        {
            // Arrange
            var expected = 37.3;
            var colorOne = Color.FromRgb(69, 74, 79);
            var colorTwo = Color.FromRgb(174, 179, 174);
            var differenceAlgorithm = new CIE76(colorOne, colorTwo);

            // Action
            var actual = differenceAlgorithm.CalculateTheDifference();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateTheDifference_PositiveTest_69_74_79_and_11_121_206()
        {
            // Arrange
            var expected = 232.31;
            var colorOne = Color.FromRgb(69, 74, 79);
            var colorTwo = Color.FromRgb(11, 121, 206);
            var differenceAlgorithm = new CIE76(colorOne, colorTwo);

            // Action
            var actual = differenceAlgorithm.CalculateTheDifference();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateTheDifference_PositiveTest_69_74_79_and_30_80_34()
        {
            // Arrange
            var expected = 183.41;
            var colorOne = Color.FromRgb(69, 74, 79);
            var colorTwo = Color.FromRgb(30, 80, 34);
            var differenceAlgorithm = new CIE76(colorOne, colorTwo);

            // Action
            var actual = differenceAlgorithm.CalculateTheDifference();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateTheDifference_PositiveTest_69_74_79_and_8_8_8()
        {
            // Arrange
            var expected = 136.42;
            var colorOne = Color.FromRgb(69, 74, 79);
            var colorTwo = Color.FromRgb(8, 8, 8);
            var differenceAlgorithm = new CIE76(colorOne, colorTwo);

            // Action
            var actual = differenceAlgorithm.CalculateTheDifference();

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion

        #region NegativeTest

        [Fact]
        public void CalculateTheDifference_NegativeTest_69_74_79_and_173_179_174()
        {
            // Arrange
            var expected = 37.71;
            var colorOne = Color.FromRgb(69, 74, 79);
            var colorTwo = Color.FromRgb(173, 179, 174);
            var differenceAlgorithm = new CIE76(colorOne, colorTwo);

            // Action
            var actual = differenceAlgorithm.CalculateTheDifference();

            // Assert
            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void CalculateTheDifference_NegativeTest_69_74_79_and_174_180_174()
        {
            // Arrange
            var expected = 37.71;
            var colorOne = Color.FromRgb(69, 74, 79);
            var colorTwo = Color.FromRgb(174, 180, 174);
            var differenceAlgorithm = new CIE76(colorOne, colorTwo);

            // Action
            var actual = differenceAlgorithm.CalculateTheDifference();

            // Assert
            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void CalculateTheDifference_NegativeTest_69_74_79_and_174_179_175()
        {
            // Arrange
            var expected = 37.71;
            var colorOne = Color.FromRgb(69, 74, 79);
            var colorTwo = Color.FromRgb(174, 179, 175);
            var differenceAlgorithm = new CIE76(colorOne, colorTwo);

            // Action
            var actual = differenceAlgorithm.CalculateTheDifference();

            // Assert
            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void CalculateTheDifference_NegativeTest_69_74_79_and_173_180_174()
        {
            // Arrange
            var expected = 37.71;
            var colorOne = Color.FromRgb(69, 74, 79);
            var colorTwo = Color.FromRgb(173, 180, 174);
            var differenceAlgorithm = new CIE76(colorOne, colorTwo);

            // Action
            var actual = differenceAlgorithm.CalculateTheDifference();

            // Assert
            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void CalculateTheDifference_NegativeTest_69_74_79_and_174_180_175()
        {
            // Arrange
            var expected = 37.71;
            var colorOne = Color.FromRgb(69, 74, 79);
            var colorTwo = Color.FromRgb(173, 180, 175);
            var differenceAlgorithm = new CIE76(colorOne, colorTwo);

            // Action
            var actual = differenceAlgorithm.CalculateTheDifference();

            // Assert
            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void CalculateTheDifference_NegativeTest_69_74_79_and_173_179_175()
        {
            // Arrange
            var expected = 37.71;
            var colorOne = Color.FromRgb(69, 74, 79);
            var colorTwo = Color.FromRgb(173, 179, 175);
            var differenceAlgorithm = new CIE76(colorOne, colorTwo);

            // Action
            var actual = differenceAlgorithm.CalculateTheDifference();

            // Assert
            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void CalculateTheDifference_NegativeTest_69_74_79_and_173_180_175()
        {
            // Arrange
            var expected = 37.71;
            var colorOne = Color.FromRgb(69, 74, 79);
            var colorTwo = Color.FromRgb(173, 180, 175);
            var differenceAlgorithm = new CIE76(colorOne, colorTwo);

            // Action
            var actual = differenceAlgorithm.CalculateTheDifference();

            // Assert
            Assert.NotEqual(expected, actual);
        }

        #endregion
    }
}
