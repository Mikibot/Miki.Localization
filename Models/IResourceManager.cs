using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Localization.Models
{
    public interface IResourceManager
    {
        string GetString(string key);
    }
}
