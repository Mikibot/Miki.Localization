using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Miki.Localization
{
    using System.Collections.Generic;
    using Miki.Functional;

    public class ResourceManager : IResourceManager
    {
        public static readonly ResourceManager Empty = new ResourceManager(new Dictionary<string, string>());
        
        private readonly Dictionary<string, string> set;

        public ResourceManager(Dictionary<string, string> set)
        {
            this.set = set;
        }

        /// <inheritdoc />
        public Optional<string> GetString(Required<string> key)
        {
            return set.TryGetValue(key, out var val) ? val : null;
        }

        public static ResourceManager FromJson(Required<string> json)
        {
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            return new ResourceManager(dict);
        }

        public static ResourceManager FromJson(Required<Stream> stream)
        {
            using var reader = new StreamReader(stream, Encoding.UTF8, true, -1, true);
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(reader.ReadToEnd());
            return new ResourceManager(dict);
        }

        public static async ValueTask<ResourceManager> FromJsonAsync(Required<Stream> stream)
        {
            var dict = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(stream);
            return new ResourceManager(dict);
        }
    }
}
