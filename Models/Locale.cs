namespace Miki.Localization.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Locale
    {
        /// <summary>
        /// An ISO-3 country code of the current locale.
        /// </summary>
        public string CountryCode { get; }

        public IResourceManager ResourceManager { get; }

        public Locale(string countryCode, IResourceManager manager)
        {
            this.CountryCode = countryCode;
            this.ResourceManager = manager;
        }

        public string GetString(string key)
            => ResourceManager.GetString(key);

        public override bool Equals(object obj)
        {
            return obj is Locale locale 
                && this.CountryCode == locale.CountryCode;
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
