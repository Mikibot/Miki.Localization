using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

public static class LanguageDatabase
{
	private static Dictionary<string, ResourceManager> Locales = new Dictionary<string, ResourceManager>();
	private static string _defaultLocale;

	public static void AddLanguage(string info, ResourceManager resource, bool isDefault = false)
	{
		Locales.Add(info, resource);

		if(isDefault)
		{
			_defaultLocale = info;
		}
	}

	public static ResourceManager GetDefaultLanguage()
	{
		if(_defaultLocale == null)
		{
			return null;
		}
		return Locales[_defaultLocale];
	}

	public static ResourceManager GetLanguageOrDefault(CultureInfo info)
	{
		return GetLanguageOrDefault(info.ThreeLetterISOLanguageName);
	}
	public static ResourceManager GetLanguageOrDefault(string iso)
	{
		if (Locales.TryGetValue(iso, out var resource))
		{
			return resource;
		}

		if(_defaultLocale != null)
		{
			return Locales[_defaultLocale];
		}

		return null;
	}
}

public class LocaleInstance
{
	// TODO: replace with IResource.
	private const string defaultResult = "error_resource_missing";

	private readonly ResourceManager _resources;

	public LocaleInstance(string isoLanguage)
	{
		_resources = LanguageDatabase.GetLanguageOrDefault(isoLanguage);

		if(_resources == null)
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

		return output ?? defaultResult;
	}

	public bool HasString(string resourceId)
		=> !string.IsNullOrWhiteSpace(_resources.GetString(resourceId));

	private static bool InternalStringAvailable(string m, ResourceManager lang)
		=> lang.GetString(m) != null;

	private static string InternalGetString(string m, ResourceManager lang, params object[] p)
	{
		string resource = lang.GetString(m);

		if(string.IsNullOrEmpty(resource))
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