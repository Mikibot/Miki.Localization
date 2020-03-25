namespace Miki.Localization
{
    using Miki.Functional;

    public interface IResourceManager
    {
        Optional<string> GetString(Required<string> key);
    }
}
