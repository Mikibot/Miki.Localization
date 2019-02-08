using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Localization
{
    public interface IResourceManager
    {
        string GetString(string key);
    }
}
