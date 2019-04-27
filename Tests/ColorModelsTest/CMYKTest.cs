namespace Tests.ColorModelsTest
{
    using System;
    using Xunit;
    using ColorModels;

    public class CMYKTest
    {   
        [Fact]
        public void Test_Convert_Cmyk_To_Rgb()
        {
            // Arrange
            CMYK cmyk = CMYK.Set(52, 1, 10, 25);
            RGB expected = RGB.Set(92, 189, 172);

            // Act
            RGB actual = cmyk.ToRgb();

            // Assert
            Assert.Equal(expected.R, actual.R);
        }
    }
}
