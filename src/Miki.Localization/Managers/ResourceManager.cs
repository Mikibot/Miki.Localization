namespace Miki.Localization
{
    using System.Collections.Generic;
    using Miki.Functional;

    public class ResourceManager : IResourceManager
    {
        private readonly Dictionary<string, string> set;

        public ResourceManager(Dictionary<string, string> set)
        {
            this.set = set;
        }

        /// <inheritdoc />
        public Optional<string> GetString(Required<string> key)
        {
            return set.TryGetValue(key, out var val) ? val : null;
        }
    }
}
