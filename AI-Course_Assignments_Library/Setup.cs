using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI_Course_Assignments_Library
{
    public class Setup
    {
        //Static objects
        private static ComputerVisionClient CvClient;

        //Client Setups
        public static TextAnalyticsClient TextAnalyticsClient()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            SetEncoding();

            return CreateTaClient(GetKey(configuration), GetEndpoint(configuration));
        }
        public static ComputerVisionClient ComputerVisionClient()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();

            return AuthCvClient(GetKey(configuration), GetEndpoint(configuration));
        }

        //Get Config from appsettings
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

        private static TextAnalyticsClient CreateTaClient(string cogSvcKey, string cogSvcEndpoint)
        {
            AzureKeyCredential credentials = new AzureKeyCredential(cogSvcKey);
            Uri endpoint = new Uri(cogSvcEndpoint);

            return new TextAnalyticsClient(endpoint, credentials);
        }
        private static ComputerVisionClient AuthCvClient(string cogSvcKey, string cogSvcEndpoint)
        {
            ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(cogSvcKey);
            CvClient = new ComputerVisionClient(credentials)
            {
                Endpoint = cogSvcEndpoint
            };

            return CvClient;
        }
    }
}
