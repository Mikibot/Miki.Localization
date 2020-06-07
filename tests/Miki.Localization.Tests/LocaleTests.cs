using System.Collections.Generic;
using Xunit;

namespace Miki.Localization.Tests
{
    public class LocaleTests
    {
        private static readonly Locale DutLocale = new Locale("dut", ResourceManager.Empty);
        private static readonly Locale SecondDutLocale = new Locale("dut", ResourceManager.Empty);
        private static readonly Locale OtherDutLocale = new Locale("dut", new ResourceManager(new Dictionary<string, string>()));
        private static readonly Locale EngLocale = new Locale("eng", ResourceManager.Empty);
        
        [Fact]
        public void EqualTest()
        {
            Assert.Equal(DutLocale, SecondDutLocale);
            Assert.NotEqual(DutLocale, OtherDutLocale);
            Assert.NotEqual(DutLocale, EngLocale);
        }
        
        [Fact]
        public void HashCodeTest()
        {
            Assert.Equal(DutLocale.GetHashCode(), SecondDutLocale.GetHashCode());
            Assert.NotEqual(DutLocale.GetHashCode(), OtherDutLocale.GetHashCode());
            Assert.NotEqual(DutLocale.GetHashCode(), EngLocale.GetHashCode());
        }
    }
}