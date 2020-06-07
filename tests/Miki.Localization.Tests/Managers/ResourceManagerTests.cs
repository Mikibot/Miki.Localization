using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Miki.Localization.Tests.Managers
{
    public class ResourceManagerTests
    {
        [Fact]
        public static void FromDictionaryTest()
        {
            var resource = new LanguageResource("greet", "Miki");
            var resourceManager = new ResourceManager(new Dictionary<string, string>
            {
                ["greet"] = "Hello {0}!"
            });
            
            Assert.Equal("Hello Miki!", resource.Get(resourceManager));
            Assert.False(resourceManager.GetString("invalid").HasValue);
        }
        
        [Fact]
        public static void FromJson()
        {
            var resource = new LanguageResource("greet", "Miki");
            var resourceManager = ResourceManager.FromJson(@"{""greet"": ""Hello {0}!""}");
            
            Assert.Equal("Hello Miki!", resource.Get(resourceManager));
            Assert.False(resourceManager.GetString("invalid").HasValue);
        }
        
        [Fact]
        public static void FromJsonStream()
        {
            using var stream = File.Open("Resources/eng.json", FileMode.Open, FileAccess.Read, FileShare.Read);
            var resource = new LanguageResource("greet", "Miki");
            var resourceManager = ResourceManager.FromJson(stream);
            
            Assert.Equal("Hello Miki!", resource.Get(resourceManager));
            Assert.False(resourceManager.GetString("invalid").HasValue);
        }
        
        [Fact]
        public static async ValueTask FromJsonStreamAsync()
        {
            await using var stream = File.Open("Resources/eng.json", FileMode.Open, FileAccess.Read, FileShare.Read);
            var resource = new LanguageResource("greet", "Miki");
            var resourceManager = await ResourceManager.FromJsonAsync(stream);
            
            Assert.Equal("Hello Miki!", resource.Get(resourceManager));
            Assert.False(resourceManager.GetString("invalid").HasValue);
        }
    }
}