namespace Tests.TestColorModels
{
    using System;
    using Xunit;
    using ColorModels;

    public class TestCMYK
    {   
        [Fact]
        public void TestCmy()
        {
            // arrange
            CMYK cmyk = new CMYK(33, 12, 32, 1);
            CMY expected = new CMY(0.33670, 0.12880, 0.32680);

            // action
            CMY actual = cmyk.Cmy();

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
