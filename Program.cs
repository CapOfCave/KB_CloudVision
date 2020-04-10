using Google.Cloud.Vision.V1;
using System;
using System.Linq;

using Google.Cloud.Vision.V1;
using System;
using System.Linq;

namespace KB_CloudVision
{
    class Program
    {
        static readonly string s_usage = @"dotnet run image-file

    Use the Google Cloud Vision API to detect faces in the image.
    Writes an output file called image-file.faces.
    ";
        public static void Main(string[] args)
        {


            // Instantiates a client
            var client = ImageAnnotatorClient.Create();
            // Load the image file into memory
            var image = Image.FromFile("F:\\img\\img1.jpg");
            // Performs label detection on the image file
            var response = client.DetectLabels(image);
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                    Console.WriteLine(annotation.Description);
            }
        }
    }
}
