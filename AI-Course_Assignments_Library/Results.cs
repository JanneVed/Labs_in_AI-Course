using Azure.AI.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Course_Assignments_Library
{
    public class Results
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

        public static async Task ImageAnalysisResult(ComputerVisionClient client, string imageSource)
        {
            Console.WriteLine($"Selected File: {imageSource}");

            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
            {
                 VisualFeatureTypes.Description,
                 VisualFeatureTypes.Tags,
                 VisualFeatureTypes.Categories,
                 VisualFeatureTypes.Brands,
                 VisualFeatureTypes.Objects,
                 VisualFeatureTypes.Adult
            };
            
            using (var imageData = File.OpenRead(imageSource))
            {
                var analysis = await client.AnalyzeImageInStreamAsync(imageData, features);
                // get image captions
                foreach (var caption in analysis.Description.Captions)
                {
                    Console.WriteLine($"Description: {caption.Text} (confidence: {caption.Confidence.ToString("P")})");
                }

                // Get image tags
                if (analysis.Tags.Count > 0)
                {
                    Console.WriteLine("Tags:");
                    foreach (var tag in analysis.Tags)
                    {
                        Console.WriteLine($" -{tag.Name} (confidence: {tag.Confidence.ToString("P")})");
                    }
                }

                // Get image categories (including celebrities and landmarks)
                List<LandmarksModel> landmarks = new List<LandmarksModel> { };
                List<CelebritiesModel> celebrities = new List<CelebritiesModel> { };
                Console.WriteLine("Categories:");
                foreach (var category in analysis.Categories)
                {
                    // Print the category
                    Console.WriteLine($" -{category.Name} (confidence: {category.Score.ToString("P")})");
                    // Get landmarks in this category
                    if (category.Detail?.Landmarks != null)
                    {
                        foreach (LandmarksModel landmark in category.Detail.Landmarks)
                        {
                            if (!landmarks.Any(item => item.Name == landmark.Name))
                            {
                                landmarks.Add(landmark);
                            }
                        }
                    }
                    // Get celebrities in this category
                    if (category.Detail?.Celebrities != null)
                    {
                        foreach (CelebritiesModel celebrity in category.Detail.Celebrities)
                        {
                            if (!celebrities.Any(item => item.Name == celebrity.Name))
                            {
                                celebrities.Add(celebrity);
                            }
                        }
                    }
                }
                // If there were landmarks, list them
                if (landmarks.Count > 0)
                {
                    Console.WriteLine("Landmarks:");
                    foreach (LandmarksModel landmark in landmarks)
                    {
                        Console.WriteLine($" -{landmark.Name} (confidence: {landmark.Confidence.ToString("P")})");
                    }
                }
                // If there were celebrities, list them
                if (celebrities.Count > 0)
                {
                    Console.WriteLine("Celebrities:");
                    foreach (CelebritiesModel celebrity in celebrities)
                    {
                        Console.WriteLine($" -{celebrity.Name} (confidence:  {celebrity.Confidence.ToString("P")})");
                    }
                }

                // Get brands in the image
                if (analysis.Brands.Count > 0)
                {
                    Console.WriteLine("Brands:");
                    foreach (var brand in analysis.Brands)
                    {
                        Console.WriteLine($" -{brand.Name} (confidence: {brand.Confidence.ToString("P")})");
                    }
                }

                // Get objects in the image
                await FileWorkers.CreateFiles.CreateObjectImage(analysis, imageSource);

                // Get moderation ratings
                string ratings = $"Ratings:\n -Adult: {analysis.Adult.IsAdultContent}\n -Racy: {analysis.Adult.IsRacyContent}\n -Gore: {analysis.Adult.IsGoryContent}";
                Console.WriteLine(ratings);
            }
        }

        
    }
}
