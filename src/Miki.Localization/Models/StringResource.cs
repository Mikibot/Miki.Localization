using Miki.Functional;

namespace Miki.Localization
{
    /// <summary>
    /// Raw string resources.
    /// </summary>
    public class StringResource : IResource
    {
        public string Value { get; internal set; }

        public StringResource(string value)
        {
            Value = value;
        }

        /// <inheritdoc/>
        public Optional<string> Get(IResourceManager instance) => Value;
    }
}
