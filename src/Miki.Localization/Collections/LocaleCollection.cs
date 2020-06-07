using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Miki.Functional;

namespace Miki.Localization.Collections
{
    public class LocaleCollection : ILocaleCollection
    {
        private readonly Dictionary<string, AggregateResourceManager> resourceManagers;

        public LocaleCollection()
        {
            resourceManagers = new Dictionary<string, AggregateResourceManager>();
        }

        public IReadOnlyCollection<string> CountryCodes => resourceManagers.Keys;

        public void Add(Locale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));
            
            Add(locale.CountryCode, locale.ResourceManager);
        }

        public void Add(string countryCode, IResourceManager resourceManager)
        {
            if (countryCode == null) throw new ArgumentNullException(nameof(countryCode));
            if (resourceManager == null) throw new ArgumentNullException(nameof(resourceManager));

            if (!resourceManagers.TryGetValue(countryCode, out var aggregateResourceManager))
            {
                aggregateResourceManager = new AggregateResourceManager();
                resourceManagers.Add(countryCode, aggregateResourceManager);
            }
            
            aggregateResourceManager.Add(resourceManager);
        }

        public Optional<string> GetString(string countryCode, string name)
        {
            return resourceManagers.TryGetValue(countryCode, out var resourceManager)
                ? resourceManager.GetString(name)
                : Optional<string>.None; 
        }

        public Optional<Locale> GetLocale(string countryCode)
        {
            return resourceManagers.TryGetValue(countryCode, out var resourceManager)
                ? new Locale(countryCode, resourceManager)
                : default;
        }

        public bool TryGetLocale(string countryCode, out Locale locale)
        {
            if (!resourceManagers.TryGetValue(countryCode, out var resourceManager))
            {
                locale = default;
                return false;
            }

            locale = new Locale(countryCode, resourceManager);
            return true;
        }

        public IEnumerator<Locale> GetEnumerator()
        {
            return resourceManagers
                .Select(kv => new Locale(kv.Key, kv.Value))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}