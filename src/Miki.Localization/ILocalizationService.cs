namespace Miki.Localization
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILocalizationService
    {
        void AddLocale(Locale locale);

        // TODO (velddev): create a core identifier interface?
        ValueTask<Locale> GetLocaleAsync(long id);

        ValueTask SetLocaleAsync(long id, string iso3);

        /// <summary>
        /// Lists all possible locales.
        /// </summary>
        /// <returns>
        /// A list of all enabled translations of Miki.
        /// </returns>
        IAsyncEnumerable<string> ListLocalesAsync();
    }
}