namespace Miki.Localization.Exceptions
{
    using System;

    public abstract class LocalizedException : Exception
    {
		public abstract IResource LocaleResource { get; }

        public LocalizedException()
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
