using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AI_Course_Assignments_Library.FileWorkers
{
    public class CreateFiles
    {
        public static async Task CreateThumbnail(ComputerVisionClient client,string imageSource)
        {
            Console.WriteLine("Generating thumbnail");

            // Generate a thumbnail
            using (var imageData = File.OpenRead(imageSource))
            {
                var thumbnailSize = UserInputs.ThumbnailSize();
                // Get thumbnail data
                var thumbnailStream = await client.GenerateThumbnailInStreamAsync(thumbnailSize[0],
               thumbnailSize[1], imageData, true);
                // Save thumbnail image
                string thumbnailFileName = "thumbnail.png";
                using (Stream thumbnailFile = File.Create(thumbnailFileName))
                {
                    thumbnailStream.CopyTo(thumbnailFile);
                }
                Console.WriteLine($"Thumbnail saved in {thumbnailFileName}");
            }
        }

        public static async Task CreateObjectImage(ImageAnalysis analysis, string imageSource)
        {
            if (analysis.Objects.Count > 0)
            {
                Console.WriteLine("Objects in image:");
                // Prepare image for drawing
                Image image = Image.FromFile(imageSource);
                Graphics graphics = Graphics.FromImage(image);
                Pen pen = new Pen(Color.Cyan, 3);
                Font font = new Font("Arial", 16);
                SolidBrush brush = new SolidBrush(Color.Black);
                foreach (var detectedObject in analysis.Objects)
                {
                    // Print object name
                    Console.WriteLine($" -{detectedObject.ObjectProperty} (confidence: {detectedObject.Confidence.ToString("P")})");
                    // Draw object bounding box
                    var r = detectedObject.Rectangle;
                    Rectangle rect = new Rectangle(r.X, r.Y, r.W, r.H);
                    graphics.DrawRectangle(pen, rect);
                    graphics.DrawString(detectedObject.ObjectProperty, font, brush, r.X, r.Y);
                }
                // Save annotated image
                String output_file = "objects.jpg";
                image.Save(output_file);
                Console.WriteLine(" Results saved in " + output_file);
            }
        }
    }
}
