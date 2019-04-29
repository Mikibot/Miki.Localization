using System;

namespace Miki.Localization
{
	public interface IResource
	{
		string Get(IResourceManager instance);
	}

	public class StringResource : IResource
	{
		public string Value { get; internal set; }

		public StringResource(string value)
		{
			Value = value;
		}

		public string Get(IResourceManager instance)
			=> Value;
	}

	public class LanguageResource : IResource
	{
		public string Resource { get; internal set; }
		public object[] Parameters { get; internal set; }

		public LanguageResource(string resource, params object[] param)
		{
			Resource = resource;
			Parameters = param;
		}

		public string Get(IResourceManager instance)
			=> string.Format(instance.GetString(Resource), Parameters);
	}
}