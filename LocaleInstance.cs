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
}