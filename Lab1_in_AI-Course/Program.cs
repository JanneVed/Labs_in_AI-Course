using Azure.AI.TextAnalytics;
using System;

namespace Lab1_in_AI_Course
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Cognetive client with key and endpoint.
            TextAnalyticsClient cogClient = Setups.GetConfig();

            Console.WriteLine("Hello and Welcome!\nAdd a text and see if I can understand which language it is.");
            string inputText = Console.ReadLine();
            Results.LanguageDetectionResult(cogClient, inputText);

            System.Diagnostics.Process.Start("questions.cmd");
        }
    }
}
