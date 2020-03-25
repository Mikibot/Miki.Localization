namespace Miki.Localization.Tests
{
    using Xunit;

    public class LanguageResourceTests
    {
        private readonly IResourceManager manager = TestResourceManager.PassThrough;

        [Fact]
        public void LanguageResourceNoParamsTest()
        {
            var resource = new LanguageResource("test");

            Assert.Equal("test", resource.Get(manager));
        }

        [Fact]
        public void LanguageResourceWithParamsTest()
        {
            var resource = new LanguageResource("test {0}", "value");

            Assert.Equal("test value", resource.Get(manager));
        }

        [Fact]
        public void LanguageResourceWithIResourceParamsTest()
        {
            var resource = new LanguageResource("test {0}", new StringResource("value"));

            Assert.Equal("test value", resource.Get(manager));
        }
    }
}
