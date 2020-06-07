using System.Collections.Generic;
using Miki.Localization.Exceptions;
using Xunit;

namespace Miki.Localization.Tests.Managers
{
    public class AggregateResourceManagerTests
    {
        [Fact]
        public static void FromSingle()
        {
            var resource = new LanguageResource("greet", "Miki");
            var resourceManager = new ResourceManager(new Dictionary<string, string>
            {
                ["greet"] = "Hello {0}!"
            });
            
            var aggregateResourceManager = new AggregateResourceManager(resourceManager);
            
            Assert.Equal("Hello Miki!", resource.Get(aggregateResourceManager));
            Assert.False(aggregateResourceManager.GetString("invalid").HasValue);
        }
        
        
        [Fact]
        public static void FromMultiple()
        {
            var greetResource = new LanguageResource("greet", "Miki");
            var farewellResource = new LanguageResource("farewell", "Miki");
            
            var resourceManager = new ResourceManager(new Dictionary<string, string>
            {
                ["greet"] = "Hello {0}!"
            });
            
            var secondResourceManager = new ResourceManager(new Dictionary<string, string>
            {
                ["greet"] = "Second hello {0}!",
                ["farewell"] = "Farewell {0}!"
            });
            
            var aggregateResourceManager = new AggregateResourceManager(resourceManager, secondResourceManager);
            
            Assert.Equal("Hello Miki!", greetResource.Get(aggregateResourceManager));
            Assert.Equal("Farewell Miki!", farewellResource.Get(aggregateResourceManager));
            Assert.False(aggregateResourceManager.GetString("invalid").HasValue);
        }
    }
}