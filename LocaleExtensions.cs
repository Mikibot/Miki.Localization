namespace Miki.Localization
{
    using Miki.Localization.Models;

    public static class LocaleExtensions
    {
        public static string GetString(this Locale locale, string key, params object[] format)
        {
            return string.Format(locale.GetString(key), format);
        }

        public static string Get(this IResource resource, Locale locale)
        {
            return resource.Get(locale.ResourceManager);
        }
    }
}
