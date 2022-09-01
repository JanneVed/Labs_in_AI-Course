using AI_Course_Assignments_Library;
using AI_Course_Assignments_Library.FileWorkers;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System;
using System.Threading.Tasks;

namespace Lab2_in_AI_Course
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = Setup.ComputerVisionClient();
            var imageSource = UserInputs.ImageSource();

            await Results.ImageAnalysisResult(client, imageSource);
            await CreateFiles.CreateThumbnail(client, imageSource);
        }
    }
}
