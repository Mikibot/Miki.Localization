using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace Miki.Localization
{
    public static class LanguageDatabase
    {
        private static readonly Dictionary<string, IResourceManager> Locales = new Dictionary<string, IResourceManager>();
        private static string _defaultLocale;

        public static void AddLanguage(string info, IResourceManager resource, bool isDefault = false)
        {
            Locales.Add(info, resource);

            if (isDefault)
            {
                _defaultLocale = info;
            }
        }

        public static IResourceManager GetDefaultLanguage()
        {
            if (_defaultLocale == null)
            {
                return null;
            }
            return Locales[_defaultLocale];
        }

        public static IResourceManager GetLanguageOrDefault(CultureInfo info)
        {
            return GetLanguageOrDefault(info.ThreeLetterISOLanguageName);
        }
        public static IResourceManager GetLanguageOrDefault(string iso)
        {
            if (Locales.TryGetValue(iso, out var resource))
            {
                return resource;
            }

            if (_defaultLocale != null)
            {
                return Locales[_defaultLocale];
            }

            return null;
        }

        public static void SetDefault(string iso)
        {
            _defaultLocale = iso;
        }
    }

    public class LocaleInstance
    {
        // TODO: replace with IResource.
        private const string defaultResult = "error_resource_missing";

        private readonly IResourceManager _resources;

        public LocaleInstance(string isoLanguage)
        {
            _resources = LanguageDatabase.GetLanguageOrDefault(isoLanguage);

            if (_resources == null)
            {
                throw new Exception("This language is not loaded correctly.");
            }
        }
        public LocaleInstance(CultureInfo info)
            : this(info.ThreeLetterISOLanguageName)
        { }

        public string GetString(string m, params object[] p)
        {
            string output = null;

            if (InternalStringAvailable(m, _resources))
            {
                output = InternalGetString(m, _resources, p);
            }
            else if (InternalStringAvailable(m, LanguageDatabase.GetDefaultLanguage()))
            {
                output = InternalGetString(m, LanguageDatabase.GetDefaultLanguage(), p);
            }

            return output ?? defaultResult;
        }

        public bool HasString(string resourceId)
            => !string.IsNullOrWhiteSpace(_resources.GetString(resourceId));

        private static bool InternalStringAvailable(string m, IResourceManager lang)
            => !string.IsNullOrWhiteSpace(lang.GetString(m));

        private static string InternalGetString(string m, IResourceManager lang, params object[] p)
        {
            string resource = lang.GetString(m);

            if (string.IsNullOrEmpty(resource))
            {
                return defaultResult;
            }

            if (p.Length > 0)
            {
                return string.Format(resource, p);
            }
            return resource;
        }
    }
}