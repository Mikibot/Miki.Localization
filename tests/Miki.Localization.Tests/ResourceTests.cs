using System.Collections.Generic;
using Miki.Localization.Collections;
using Xunit;

namespace Miki.Localization.Tests
{
    public class ResourceTests
    {
        [Fact]
        public void GetTest()
        {
            var resourceManager = new ResourceManager(new Dictionary<string, string>
            {
                ["greet"] = "Hello {0}!"
            });
            var locale = new Locale("eng", resourceManager);
            var collection = new LocaleCollection { locale };
            
            var resource = new LanguageResource("greet", "Miki");

            Assert.Equal("Hello Miki!", resource.Get(resourceManager));
            Assert.Equal("Hello Miki!", resource.Get(locale));
            Assert.Equal("Hello Miki!", resource.Get(collection, "eng"));
            
            Assert.Equal("Hello Miki!", resourceManager.GetString("greet", "Miki"));
            Assert.Equal("Hello Miki!", locale.GetString("greet", "Miki"));
            Assert.Equal("Hello Miki!", collection.GetString("eng", "greet", "Miki"));
        }
        
        [Fact]
        public void GetMissingStringTest()
        {
            var resource = new LanguageResource("invalid");
            var locale = new Locale("eng", ResourceManager.Empty);
            var collection = new LocaleCollection { locale };
            
            Assert.False(resource.Get(ResourceManager.Empty).HasValue);
            Assert.False(resource.Get(locale).HasValue);
            Assert.False(resource.Get(collection, "eng").HasValue);
            
            Assert.False(ResourceManager.Empty.GetString("invalid").HasValue);
            Assert.False(locale.GetString("invalid").HasValue);
            Assert.False(collection.GetString("eng", "invalid").HasValue);
        }
    }
}