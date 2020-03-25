namespace Miki.Localization
{
    using System;
    using Miki.Functional;

    public class TestResourceManager : IResourceManager
    {
        private readonly Func<string, string> keyFactory;

        public TestResourceManager(Func<string, string> keyFactory)
        {
            this.keyFactory = keyFactory;
        }

        public static TestResourceManager PassThrough => new TestResourceManager(x => x);
        
        /// <inheritdoc />
        public Optional<string> GetString(Required<string> key)
        {
            return keyFactory(key);
        }
    }
}
