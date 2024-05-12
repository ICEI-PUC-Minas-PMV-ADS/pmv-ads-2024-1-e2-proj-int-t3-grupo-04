namespace NextMidiaApi.Configuration
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;


    static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager()
        {
            // Pegar dados do arquivo appsettings
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
