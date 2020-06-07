using System.Linq;
using Miki.Functional;

namespace Miki.Localization
{
    using System.Collections.Generic;

    /// <summary>
    /// A 
    /// </summary>
    public struct LanguageResource : IResource
    {
        private readonly string resource;
        private readonly object[] parameters;

        public LanguageResource(string resource, params object[] parameters)
        {
            this.resource = resource;
            this.parameters = parameters;
        }

        /// <inheritdoc/>
        public Optional<string> Get(IResourceManager instance)
        {
            var result = instance.GetString(resource);

            if (!result.HasValue)
            {
                return Optional<string>.None;
            }
            
            return string.Format(result, InterpolateResources(instance, parameters).ToArray());
        }

        private static IEnumerable<object> InterpolateResources(IResourceManager resourceManager, IEnumerable<object> parameters)
        {
            return parameters.Select(p => p is IResource resource ? resource.Get(resourceManager).UnwrapDefault() : p);
        }
    }
}
