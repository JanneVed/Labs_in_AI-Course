using AI_Course_Assignments_Library;
using Azure.AI.TextAnalytics;
using System;

namespace Lab1_in_AI_Course
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = Setup.TextAnalyticsClient();
            var userInput = UserInputs.TextToIdentify();

            Results.LanguageDetectionResult(client, userInput);

            System.Diagnostics.Process.Start("questions.cmd", $"{UserInputs.Question()}");
        }
    }
}
