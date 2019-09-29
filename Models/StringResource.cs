namespace Miki.Localization.Models
{
    public class StringResource : IResource
    {
        public string Value { get; internal set; }

        public StringResource(string value)
        {
            Value = value;
        }

        public string Get(IResourceManager instance)
            => Value;
    }
}
