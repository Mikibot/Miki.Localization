using Miki.Functional;
using Miki.Localization.Collections;

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
        public static Optional<string> GetString(
            this ILocaleCollection collection,
            string countryCode,
            string key,
            params object[] format)
        {
            return collection.TryGetLocale(countryCode, out var locale)
                ? locale.GetString(new LanguageResource(key, format))
                : null;
        }
        
        /// <summary>
        /// Helper extension function to automatically format a string using string.Format
        /// </summary>
        public static Optional<string> GetString(
            this IResourceManager resourceManager,
            string key,
            params object[] format)
        {
            return new LanguageResource(key, format).Get(resourceManager);
        }
        
        /// <summary>
        /// Helper extension function to automatically format a string using string.Format
        /// </summary>
        public static Optional<string> GetString(this Locale locale, string key, params object[] format)
        {
            return locale.GetString(new LanguageResource(key, format));
        }

        /// <summary>
        /// Get helper function that fetches the resource manager from a locale collection.
        /// </summary>
        public static Optional<string> Get(this IResource resource, ILocaleCollection collection, string countryCode)
        {
            return collection.TryGetLocale(countryCode, out var locale) ? locale.GetString(resource) : null;
        }

        /// <summary>
        /// Get helper function that fetches the resource manager from a locale.
        /// </summary>
        public static Optional<string> Get(this IResource resource, Locale locale)
        {
            return resource.Get(locale.ResourceManager);
        }
    }
}