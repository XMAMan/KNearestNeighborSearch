using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NearestNeighborSearch;

namespace Main
{
    class KdTreeTest2D
    {
        private IPointSearch pointSearch;
        private Bitmap image;

        public KdTreeTest2D(int pointCount, int width, int height)
        {
            IPoint[] points = new IPoint[pointCount];
            Random rand = new Random(0);
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Point2D(rand.Next(width), rand.Next(height));
            }

            this.pointSearch = new BdTree(points, 2);
            //this.pointSearch = new KdTree(points, 2);
            //this.pointSearch = new PointSearchBruthForce(points, 2);
            this.image = GetImage(points, width, height);
        }

        private static Bitmap GetImage(IPoint[] points, int width, int height)
        {
            Bitmap image = new Bitmap(width, height);

            Graphics grx = Graphics.FromImage(image);
            grx.Clear(Color.White);

            int pointSize = 1;
            foreach (var point in points)
            {
                grx.FillEllipse(Brushes.Black, point[0] - pointSize, point[1] - pointSize, pointSize * 2, pointSize * 2);
            }
            grx.Dispose();

            return image;
        }

        public Bitmap GetImage()
        {
            return this.image;
        }

        public IPoint[] ApproximateKNearestNeighborSearch(int x, int y, int k)
        {
            return this.pointSearch.ApproximateKNearestNeighborSearch(new Point2D(x, y), k);
        }

        public IPoint[] PriorityKNearestNeighborSearch(int x, int y, int k)
        {
            return this.pointSearch.PriorityKNearestNeighborSearch(new Point2D(x, y), k);
        }

        public IPoint[] FixedRadiusSearchForKNearestNeighbors(int x, int y, float radius, int k)
        {
            return this.pointSearch.FixedRadiusSearchForKNearestNeighbors(new Point2D(x, y), radius, k);
        }
    }
}
