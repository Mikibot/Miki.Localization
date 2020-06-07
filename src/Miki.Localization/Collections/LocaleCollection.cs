using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
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

        public static LocaleCollection FromAssembly(
            Assembly assembly,
            string @namespace,
            Func<IResourceManager, IResourceManager> configure = null)
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
                
                IResourceManager resourceManager = ResourceManager.FromJson(stream);

                if (configure != null)
                {
                    resourceManager = configure(resourceManager);
                }

                collection.Add(localeName, resourceManager);
            }

            return collection;
        }

        public static LocaleCollection FromDirectory(
            string path,
            Func<IResourceManager, IResourceManager> configure = null)
        {
            var collection = new LocaleCollection();
            var files = Directory.GetFiles(path, "*.json");
            
            foreach(var fileName in files)
            {
                using var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                var languageName = Path.GetFileNameWithoutExtension(fileName);
                IResourceManager resourceManager = ResourceManager.FromJson(stream);

                if (configure != null)
                {
                    resourceManager = configure(resourceManager);
                }

                collection.Add(languageName, resourceManager);
            }
            
            return collection;
        }

        public static async ValueTask<LocaleCollection> FromDirectoryAsync(
            string path,
            Func<IResourceManager, IResourceManager> configure = null)
        {
            var collection = new LocaleCollection();
            var files = Directory.GetFiles(path, "*.json");
            
            foreach(var fileName in files)
            {
                await using var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                var languageName = Path.GetFileNameWithoutExtension(fileName);
                IResourceManager resourceManager = await ResourceManager.FromJsonAsync(stream);

                if (configure != null)
                {
                    resourceManager = configure(resourceManager);
                }
                
                collection.Add(languageName, resourceManager);
            }
            
            return collection;
        }
    }
}