using System.Collections.Generic;
using Miki.Functional;

namespace Miki.Localization.Collections
{
    public interface ILocaleCollection : IEnumerable<Locale>
    {
        IReadOnlyCollection<string> CountryCodes { get; }
        
        void Add(Locale locale);
        
        void Add(string countryCode, IResourceManager resourceManager);

        Optional<string> GetString(string countryCode, string name);

        Optional<Locale> GetLocale(string countryCode);
        
        bool TryGetLocale(string countryCode, out Locale locale);
    }
}