namespace Miki.Localization
{
    using System;
    using Miki.Functional;

    public class ResxResourceManager : IResourceManager
    {
        private readonly System.Resources.ResourceManager resourceManager;

        public ResxResourceManager(Type type)
            : this(new System.Resources.ResourceManager(type))
        {
        }
        
        public ResxResourceManager(System.Resources.ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public Optional<string> GetString(Required<string> key)
            => resourceManager.GetString(key);
    }
}
