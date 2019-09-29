namespace Miki.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Resources;
    using System.Text;
    using Miki.Localization.Models;

    public class ResxResourceManager : IResourceManager
    {
        readonly ResourceManager rm;

        public ResxResourceManager(Type type)
        {
            rm = new ResourceManager(type);
        }
        public ResxResourceManager(ResourceManager resourceManager)
        {
            rm = resourceManager;
        }

        public string GetString(string key)
            => rm.GetString(key);
    }
}
