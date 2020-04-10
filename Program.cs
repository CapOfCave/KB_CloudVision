using System;

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
            if (args.Length < 1)
            {
                Console.WriteLine(s_usage);
                return;
            }

            var client = ImageAnnotatorClient.Create();
            var response = client.DetectFaces(Image.FromFile(args[0]));

            int numberOfFacesFound = 0;
            using (var image = System.Drawing.Image.FromFile(args[0]))
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image))
            {
                var cyanPen = new System.Drawing.Pen(System.Drawing.Color.Cyan, 3);
                foreach (var annotation in response)
                {
                    g.DrawPolygon(cyanPen, annotation.BoundingPoly.Vertices.Select(
                        (vertex) => new System.Drawing.Point(vertex.X, vertex.Y)).ToArray());
                    // ...
                }
                // ...
            }
            // ...
        }
    }
}
