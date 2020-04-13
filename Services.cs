using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;

namespace KB_CloudVision
{
    public sealed class Services
    {

        //variables
        private readonly System.Drawing.Pen cyanPen = new System.Drawing.Pen(System.Drawing.Color.Cyan, 3);
        private readonly System.Drawing.Pen redPen = new System.Drawing.Pen(System.Drawing.Color.Red, 1);
        private readonly string imageUrl;
        // References

        private ImageAnnotatorClient client;
        private Google.Cloud.Vision.V1.Image image;
        private IReadOnlyList<FaceAnnotation> face_response;
       


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

        internal void drawBoundedPolygon(Graphics g)
        {

            foreach (var annotation in getFaceResponse())
            {
               
                g.DrawPolygon(cyanPen, annotation.BoundingPoly.Vertices.Select(
                    (vertex) => new System.Drawing.Point(vertex.X, vertex.Y)).ToArray());

            }
        }

     

        internal void drawLandmarks(Graphics g)
        {
            foreach (var annotation in getFaceResponse())
            {
                var landmarks = annotation.Landmarks;
                foreach (var landmark in landmarks)
                {
                    drawCircle(g, redPen, new System.Drawing.Point((int)landmark.Position.X, (int)landmark.Position.Y), 1);
                }
            }
        }

        private void drawCircle(System.Drawing.Graphics drawingArea, System.Drawing.Pen penToUse, System.Drawing.Point center, int radius)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            drawingArea.DrawEllipse(penToUse, rect);
        }

        //Methods
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
                face_response = client.DetectFaces(image);
            }
            return face_response;
           
        }

    }
}
