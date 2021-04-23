using Microsoft.Extensions.Configuration;

namespace APINUnitTest.Setup
{
    public class ReadConfigFile
    {
        public string GetConfigString(string configKey)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("APIUnitTestConfig.json")
            .Build();
            return config[configKey];
        }
    }
}
