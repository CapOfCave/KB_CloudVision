using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;

namespace KB_CloudVision
{
    public sealed class Services
    {

        //variables
        private readonly System.Drawing.Pen cyanPen = new System.Drawing.Pen(System.Drawing.Color.Cyan, 5);
        private readonly System.Drawing.Pen redPen = new System.Drawing.Pen(System.Drawing.Color.Red, 3);
        private readonly string imageUrl;

        // References
        private ImageAnnotatorClient client;
        private Google.Cloud.Vision.V1.Image image;
        private IReadOnlyList<FaceAnnotation> face_response;
        private System.Drawing.Image image_processed;
        private System.Drawing.Image image_unprocessed;




        public Services(string imageUrl)
        {
            this.imageUrl = imageUrl;
        }

        //Services
        internal IEnumerable<EntityAnnotation> getLabels()
        {
            return getClient().DetectLabels(getImage()).Where(label => label.Description != null);
        }
        internal ImageProperties ColorAnalysis()
        {
            return getClient().DetectImageProperties(getImage());
        }

        internal void drawBoundedPolygon()
        {

            foreach (var annotation in getFaceResponse())
            {

                Graphics().DrawPolygon(cyanPen, annotation.BoundingPoly.Vertices.Select(
                    (vertex) => new System.Drawing.Point(vertex.X, vertex.Y)).ToArray());

            }
        }



        internal void drawLandmarks()
        {
            foreach (var annotation in getFaceResponse())
            {
                var landmarks = annotation.Landmarks;
                foreach (var landmark in landmarks)
                {
                    drawCircle(Graphics(), redPen, new System.Drawing.Point((int)landmark.Position.X, (int)landmark.Position.Y), 1);
                }
            }
        }

        internal void Reset()
        {
            if (image_unprocessed == null)
            {
                return;
            }
            image_processed = (System.Drawing.Image)image_unprocessed.Clone();
        }

        private void drawCircle(System.Drawing.Graphics drawingArea, System.Drawing.Pen penToUse, System.Drawing.Point center, int radius)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            drawingArea.DrawEllipse(penToUse, rect);
        }


        // Helper Methods
        private ImageAnnotatorClient getClient()
        {
            if (client == null)
            {
                Console.WriteLine("Connecting... ");
                client = ImageAnnotatorClient.Create();
                Console.WriteLine("Connected");
            }
            return client;
        }

        internal BitmapImage ProcessedImage()
        {
            return Convert(FileSystemImage());
        }


        private Google.Cloud.Vision.V1.Image getImage()
        {
            if (image == null)
            {
                image = Google.Cloud.Vision.V1.Image.FromFile(imageUrl);
            }

            return image;
        }

        private IEnumerable<FaceAnnotation> getFaceResponse()
        {
            if (face_response == null)
            {
                face_response = getClient().DetectFaces(getImage());
            }
            return face_response;

        }

        private System.Drawing.Image FileSystemImage()
        {
            if (image_processed == null)
            {
                image_processed = System.Drawing.Image.FromFile(imageUrl);
                image_unprocessed = (System.Drawing.Image)image_processed.Clone();
            }
            return image_processed;
        }

        private System.Drawing.Graphics Graphics()
        {
            return System.Drawing.Graphics.FromImage(FileSystemImage());
        }

        public BitmapImage Convert(System.Drawing.Image img)
        {
            using (var memory = new MemoryStream())
            {
                img.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
