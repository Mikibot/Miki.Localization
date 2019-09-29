using System;
using System.Collections.Generic;
using System.Text;
using Miki.Localization.Models;

namespace Miki.Localization.Exceptions
{
    public abstract class LocalizedException : Exception
    {
		public abstract IResource LocaleResource { get; }

        public LocalizedException()
            : base()
        {
        }
        public LocalizedException(string message)
            : base(message)
        {
        }
        public LocalizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
