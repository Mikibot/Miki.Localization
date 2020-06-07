using Miki.Localization.Collections;
using Xunit;

namespace Miki.Localization.Tests
{
    public class LocaleCollectionTest
    {
        [Fact]
        public void FromAssembly()
        {
            var assembly = typeof(LocaleCollectionTest).Assembly;
            var collection = LocaleCollection.FromAssembly(assembly, "Miki.Localization.Tests.Resources");
            
            Assert.Equal("Dutch", collection.GetString("dut", "language"));
            Assert.Equal("English", collection.GetString("eng", "language"));
        }
    }
}