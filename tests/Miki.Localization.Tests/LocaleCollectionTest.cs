using System.Collections.Generic;
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
            var didConfigure = false;

            IResourceManager Configure(IResourceManager manager)
            {
                didConfigure = true;
                return manager;
            }
            
            var assembly = typeof(LocaleCollectionTest).Assembly;
            var collection = LocaleCollection.FromAssembly(assembly, "Miki.Localization.Tests.Resources", Configure);
            
            Assert.True(didConfigure);
            Assert.NotStrictEqual(new[] {"dut", "eng"}, collection.CountryCodes);
            Assert.Equal("Dutch", collection.GetString("dut", "language"));
            Assert.Equal("English", collection.GetString("eng", "language"));
        }
        
        [Fact]
        public void FromDirectory()
        {
            var didConfigure = false;

            IResourceManager Configure(IResourceManager manager)
            {
                didConfigure = true;
                return manager;
            }
            
            var collection = LocaleCollection.FromDirectory("Resources", Configure);
            
            Assert.True(didConfigure);
            Assert.NotStrictEqual(new[] {"eng", "dut"}, collection.CountryCodes);
            Assert.Equal("Dutch", collection.GetString("dut", "language"));
            Assert.Equal("English", collection.GetString("eng", "language"));
        }
        
        [Fact]
        public async Task FromDirectoryAsync()
        {
            var didConfigure = false;

            IResourceManager Configure(IResourceManager manager)
            {
                didConfigure = true;
                return manager;
            }
            
            var collection = await LocaleCollection.FromDirectoryAsync("Resources", Configure);
            
            Assert.True(didConfigure);
            Assert.NotStrictEqual(new[] {"eng", "dut"}, collection.CountryCodes);
            
            Assert.Equal("eng", collection.GetLocale("eng").CountryCode);
            Assert.Throws<KeyNotFoundException>(() => collection.GetLocale("invalid"));
            
            Assert.True(collection.TryGetLocale("eng", out _));
            Assert.False(collection.TryGetLocale("invalid", out _));
            
            Assert.Equal("Dutch", collection.GetString("dut", "language"));
            Assert.Equal("English", collection.GetString("eng", "language"));
        }
        
        [Fact]
        public async Task LocaleGetStringAsync()
        {
            var didConfigure = false;

            IResourceManager Configure(IResourceManager manager)
            {
                didConfigure = true;
                return manager;
            }

            var collection = await LocaleCollection.FromDirectoryAsync("Resources", Configure);
            var resource = new LanguageResource("greet", "Miki");
            
            Assert.True(didConfigure);
            Assert.NotStrictEqual(new[] {"eng", "dut"}, collection.CountryCodes);
            
            Assert.Equal("eng", collection.GetLocale("eng").CountryCode);
            Assert.Throws<KeyNotFoundException>(() => collection.GetLocale("invalid"));
            
            Assert.True(collection.TryGetLocale("eng", out _));
            Assert.False(collection.TryGetLocale("invalid", out _));
            
            Assert.Equal("Hallo Miki!", resource.Get(collection, "dut"));
            Assert.Equal("Hello Miki!", resource.Get(collection, "eng"));
        }
    }
}