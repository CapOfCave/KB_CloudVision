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
        //public static void SMain(string[] args)
        //{
        //    // Instantiates a service object
        //    Services services = new Services("F:\\img\\img1.jpg");

        //    var colors = services.ColorAnalysis();
           
           
        //    // Performs label detection on the image file

        //    var labels = services.getLabels();

        //    foreach (var annotation in labels)
        //    {
        //            Console.WriteLine(annotation.Description);
        //    }
         
        //    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image_system))
        //    {
        //        services.drawBoundedPolygon();
        //        services.drawLandmarks();
        //    }
        //    image_system.Save("F:\\img\\img1-erg.jpg");




        //}
       
    }
}
