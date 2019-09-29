namespace Miki.Localization.Models
{
    public class LanguageResource : IResource
    {
        public string Resource { get; internal set; }
        public object[] Parameters { get; internal set; }

        public LanguageResource(string resource, params object[] param)
        {
            Resource = resource;
            Parameters = param;
        }

        public string Get(IResourceManager instance)
            => string.Format(instance.GetString(Resource), Parameters);
    }
}
