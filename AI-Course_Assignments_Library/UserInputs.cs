using System;
using System.Collections.Generic;
using System.Text;

namespace AI_Course_Assignments_Library
{
    public class UserInputs
    {
        public static string ImageSource()
        {
            Console.Write("Source: ");

            return Console.ReadLine();
        }

        public static string TextToIdentify()
        {
            Console.WriteLine("Hello and Welcome!\nAdd a text and see if I can understand which language it is.");

            return Console.ReadLine(); 
        }

        public static string Question()
        {
            Console.WriteLine("Ask your question.");

            return Console.ReadLine();
        }

        public static int[] ThumbnailSize()
        {
            int[] thumbnailSize = new int[2];
            Console.Write("Width: ");
            thumbnailSize[0] = Convert.ToInt32(Console.ReadLine());
            Console.Write("height: ");
            thumbnailSize[1] = Convert.ToInt32(Console.ReadLine());

            return thumbnailSize;
        }
    }
}