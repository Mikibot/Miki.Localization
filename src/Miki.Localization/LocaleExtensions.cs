namespace Miki.Localization
{
    /// <summary>
    /// Helper functions for the <see cref="Locale"/> class.
    /// </summary>
    public static class LocaleExtensions
    {
        /// <summary>
        /// Helper extension function to automatically format a string using string.Format
        /// </summary>
        public static string GetString(this Locale locale, string key, params object[] format)
        {
            return locale.GetString(new LanguageResource(key, format));
        }

        /// <summary>
        /// Get helper function that fetches the resource manager from a locale.
        /// </summary>
        public static string Get(this IResource resource, Locale locale)
        {
            return resource.Get(locale.ResourceManager);
        }
    }
}
