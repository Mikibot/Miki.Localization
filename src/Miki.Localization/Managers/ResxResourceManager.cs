namespace Miki.Localization
{
    using System;
    using Miki.Functional;

    public class ResxResourceManager : IResourceManager
    {
        private readonly System.Resources.ResourceManager rm;

        public ResxResourceManager(Type type)
        {
            rm = new System.Resources.ResourceManager(type);
        }
        public ResxResourceManager(System.Resources.ResourceManager resourceManager)
        {
            rm = resourceManager;
        }

        public Optional<string> GetString(Required<string> key)
            => rm.GetString(key);
    }
}
