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
        public string Get(IResourceManager instance)
        {
            return string.Format(
                instance.GetString(resource), InterpolateResources(instance, parameters));
        }

        private static object[] InterpolateResources(
            IResourceManager resourceManager, IEnumerable<object> parameters)
        {
            List<object> newObjects = new List<object>();
            foreach(var p in parameters)
            {
                if(p is IResource resource)
                {
                    newObjects.Add(resource.Get(resourceManager));
                    continue;
                }
                newObjects.Add(p);
            }
            return newObjects.ToArray();
        }
    }
}
