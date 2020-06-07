namespace Miki.Localization
{
    using System;

    /// <summary>
    /// Localized 
    /// </summary>
    public readonly struct Locale : IEquatable<Locale>
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
            CountryCode = countryCode;
            ResourceManager = manager;
        }
        
        /// <summary>
        /// Gets a string with the said resource from the resource manager.
        /// </summary>
        public string GetString(IResource resource) => resource.Get(ResourceManager);

        public override bool Equals(object obj)
        {
            return obj is Locale other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CountryCode, ResourceManager);
        }

        public static explicit operator Locale(string s)
        {
            return new Locale(s, null);
        }

        public bool Equals(Locale other)
        {
            return CountryCode == other.CountryCode && Equals(ResourceManager, other.ResourceManager);
        }

        public static bool operator ==(Locale left, Locale right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Locale left, Locale right)
        {
            return !left.Equals(right);
        }
    }
}
