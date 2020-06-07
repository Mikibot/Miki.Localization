using System.Collections.Generic;
using System.Linq;
using Miki.Functional;

namespace Miki.Localization
{
    public class AggregateResourceManager : IResourceManager
    {
        private readonly IList<IResourceManager> resourceManagers;

        public AggregateResourceManager()
        {
            resourceManagers = new List<IResourceManager>();
        }

        public AggregateResourceManager(IEnumerable<IResourceManager> resourceManagers)
        {
            this.resourceManagers = new List<IResourceManager>(resourceManagers);
        }

        public AggregateResourceManager(params IResourceManager[] resourceManagers)
            : this(resourceManagers.AsEnumerable())
        {
        }

        public void Add(IResourceManager resourceManager)
        {
            resourceManagers.Add(resourceManager);
        }

        /// <inheritdoc />
        public Optional<string> GetString(Required<string> key)
        {
            if (resourceManagers.Count == 1)
            {
                return resourceManagers[0].GetString(key);
            } 
            
            return resourceManagers
                .Select(resourceManager => resourceManager.GetString(key))
                .First(result => result.HasValue);
        }
    }
}