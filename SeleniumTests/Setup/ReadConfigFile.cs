using Microsoft.Extensions.Configuration;


namespace SeleniumTests.Setup
{
    public class ReadConfigFile
    {
        public string GetConfigString(string configKey)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("TestVariablesConfig.json")
            .Build();
            return config[configKey];
        }

    }
}
