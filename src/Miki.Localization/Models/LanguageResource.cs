using System.Linq;

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
        private readonly bool hasResourceParameter;

        public LanguageResource(string resource, params object[] parameters)
        {
            this.resource = resource;
            this.parameters = parameters;
            hasResourceParameter = parameters.OfType<IResource>().Any();
        }

        /// <inheritdoc/>
        public string Get(IResourceManager instance)
        {
            if (hasResourceParameter)
            {
                return string.Format(instance.GetString(resource), InterpolateResources(instance, parameters));
            }
            
            return string.Format(instance.GetString(resource), parameters);
        }

        private static IEnumerable<object> InterpolateResources(IResourceManager resourceManager, IEnumerable<object> parameters)
        {
            return parameters.Select(p => p is IResource resource ? resource.Get(resourceManager) : p);
        }
    }
}
