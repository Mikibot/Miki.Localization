using Miki.Localization.Tests.Resources;
using Xunit;

namespace Miki.Localization.Tests.Managers
{
    public class ResxResourceManagerTests
    {
        [Fact]
        public static void GetStringTest()
        {
            var resource = new LanguageResource("greet", "Miki");
            var resourceManager = new ResxResourceManager(typeof(Texts));
            
            Assert.Equal("Hello Miki!", resource.Get(resourceManager));
            Assert.False(resourceManager.GetString("invalid").HasValue);
        }
    }
}