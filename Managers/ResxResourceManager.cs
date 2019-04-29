using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace Miki.Localization
{
    public class ResxResourceManager : IResourceManager
    {
        ResourceManager rm;

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
