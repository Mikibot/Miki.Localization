using Miki.Functional;

namespace Miki.Localization
{
	public interface IResource
	{
		Optional<string> Get(IResourceManager instance);
	}
}