using Azure.AI.TextAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_in_AI_Course
{
    class Results
    {
        public static void LanguageDetectionResult(TextAnalyticsClient cogClient, string userInputs)
        {
            DetectedLanguage detectedLanguage = cogClient.DetectLanguage(userInputs);
            if (detectedLanguage.ConfidenceScore <= 0.25)
            {
                Console.WriteLine($"(Confidence Score: {detectedLanguage.ConfidenceScore})");
                Console.WriteLine($"I think it's {detectedLanguage.Name}, not sure though");
            }
            else if (detectedLanguage.ConfidenceScore >= 0.5 && detectedLanguage.ConfidenceScore < 0.75)
            {
                Console.WriteLine($"(Confidence Score: {detectedLanguage.ConfidenceScore})");
                Console.WriteLine($"I think it is: {detectedLanguage.Name}");
            }
            else if (detectedLanguage.ConfidenceScore >= 0.75 && detectedLanguage.ConfidenceScore < 1)
            {
                Console.WriteLine($"(Confidence Score: {detectedLanguage.ConfidenceScore})");
                Console.WriteLine($"I'm pretty sure it's {detectedLanguage.Name}");
            }
            else if (detectedLanguage.ConfidenceScore == 1)
            {
                Console.WriteLine($"(Confidence Score: {detectedLanguage.ConfidenceScore})");
                Console.WriteLine($"I'm 100% sure it's {detectedLanguage.Name}");
            }
        }
    }
}
