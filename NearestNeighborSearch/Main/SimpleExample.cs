using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NearestNeighborSearch;

namespace Main
{
    class SimpleExample
    {
        class Point3D : IPoint
        {
            public float[] data;

            public Point3D(float x, float y, float z)
            {
                this.data = new float[] { x, y, z };
            }

            public float this[int dimension]
            {
                get
                {
                    return this.data[dimension];
                }
            }
        }

        public static void Run()
        {
            int pointCount = 100000;
            IPoint[] points = new IPoint[pointCount];
            Random rand = new Random(0);
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Point3D(rand.Next(100), rand.Next(100), rand.Next(100));
            }

            KNearestNeighborSearch search = new KNearestNeighborSearch(points, 3); //3D-Points
            IPoint[] result = search.SearchKNearestNeighbors3D(50, 50, 50, 123); //Get the 123 nearest points
        }
    }
}
