using System.Threading.Tasks;
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
        
        [Fact]
        public void FromDirectory()
        {
            var collection = LocaleCollection.FromDirectory("Resources");
            
            Assert.Equal("Dutch", collection.GetString("dut", "language"));
            Assert.Equal("English", collection.GetString("eng", "language"));
        }
        
        [Fact]
        public async Task FromDirectoryAsync()
        {
            var collection = await LocaleCollection.FromDirectoryAsync("Resources");
            
            Assert.Equal("Dutch", collection.GetString("dut", "language"));
            Assert.Equal("English", collection.GetString("eng", "language"));
        }
    }
}