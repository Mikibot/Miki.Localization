namespace Miki.Localization
{
    using System;

    /// <summary>
    /// Localized 
    /// </summary>
    public class Locale
    {
        /// <summary>
        /// An ISO-3 country code of the current locale.
        /// </summary>
        public string CountryCode { get; }

        /// <summary>
        /// The resources attached to this Locale.
        /// </summary>
        public IResourceManager ResourceManager { get; }

        public Locale(string countryCode, IResourceManager manager)
        {
            this.CountryCode = countryCode;
            this.ResourceManager = manager;
        }
        
        /// <summary>
        /// Gets a string with the said resource from the resource manager.
        /// </summary>
        public string GetString(IResource resource) => resource.Get(ResourceManager);

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            return obj is Locale locale && this.CountryCode == locale.CountryCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.CountryCode);
        }

        public static explicit operator Locale(string s)
        {
            return new Locale(s, null);
        }
    }
}
