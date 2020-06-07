using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
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

        public static LocaleCollection FromAssembly(Assembly assembly, string @namespace)
        {
            const string suffix = ".json";
            var collection = new LocaleCollection();

            foreach (var path in assembly.GetManifestResourceNames()
                .Where(f => f.StartsWith(@namespace) && f.EndsWith(suffix)))
            {
                var localeName = path[..^suffix.Length];
                var end = localeName.LastIndexOf('.');

                if (end != -1)
                {
                    localeName = localeName.Substring(end + 1);
                }

                using var stream = assembly.GetManifestResourceStream(path)
                                   ?? throw new InvalidOperationException($"Could not find resource {path}");
                
                var resourceManager = ResourceManager.FromJson(stream);

                collection.Add(localeName, resourceManager);
            }

            return collection;
        }
    }
}