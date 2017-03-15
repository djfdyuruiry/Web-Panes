using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using WebPanes.Interface;
using WebPanes.Model;

namespace WebPanes.Provider
{
    public class YamlWebPanesConfigurationProvider : IWebPanesConfigurationProvider
    {
        private const string ConfigFileName = "WebPanesConfiguration.yaml";

        private WebPanesConfiguration _cachedConfig;

        public WebPanesConfiguration LoadConfiguration()
        {
            if (_cachedConfig != null)
            {
                return _cachedConfig;
            }

            var yamlConfig = File.ReadAllText(ConfigFileName);
            var deserialiserBuilder = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention());
            var deserialiser = deserialiserBuilder.Build();

            var config = deserialiser.Deserialize<WebPanesConfiguration>(yamlConfig);

            _cachedConfig = config;

            return config;
        }
    }
}
