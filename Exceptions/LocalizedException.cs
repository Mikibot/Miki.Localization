using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Localization.Exceptions
{
    public abstract class LocalizedException : Exception
    {
		public abstract IResource LocaleResource { get; }
    }
}
