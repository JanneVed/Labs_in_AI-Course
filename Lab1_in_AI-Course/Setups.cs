using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;

namespace Lab1_in_AI_Course
{
    class Setups
    {
        public static TextAnalyticsClient GetConfig()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            SetEncoding();
            return CreateClient(GetKey(configuration), GetEndpoint(configuration));
        }

        private static string GetKey(IConfigurationRoot configuration)
        {
            string cogSvcKey = configuration["CognitiveServiceKey"];
            return cogSvcKey;
        }

        private static string GetEndpoint(IConfigurationRoot configuration)
        {
            string cogSvcEndpoint = configuration["CognitiveServicesEndpoint"];
            return cogSvcEndpoint;
        }

        private static void SetEncoding()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
        }

        private static TextAnalyticsClient CreateClient(string cogSvcKey, string cogSvcEndpoint)
        {
            AzureKeyCredential credentials = new AzureKeyCredential(cogSvcKey);
            Uri endpoint = new Uri(cogSvcEndpoint);
            return new TextAnalyticsClient(endpoint, credentials);
        }
    }
}
