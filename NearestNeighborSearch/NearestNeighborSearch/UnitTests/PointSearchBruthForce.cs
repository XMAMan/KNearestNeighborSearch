using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NearestNeighborSearch
{
    class PointSearchBruthForce : IPointSearch
    {
        private IPoint[] points;
        private int dimension;

        public PointSearchBruthForce(IPoint[] points, int dimension)
        {
            this.points = points;
            this.dimension = dimension;
        }

        public IPoint[] ApproximateKNearestNeighborSearch(IPoint queryPoint,
                                                          int k,                // number of near neighbors to return
                                                          float eps = 0)        // the error bound
        {
            return this.points.Select(x => new KeyValuePair<float, IPoint>(PointDistanceSquared(queryPoint, x, this.dimension), x)).OrderBy(x => x.Key).Select(x => x.Value).ToList().GetRange(0, k).ToArray();
        }

        public IPoint[] PriorityKNearestNeighborSearch(IPoint queryPoint,
                                                          int k,                // number of near neighbors to return
                                                          float eps = 0)        // the error bound
        {
            return ApproximateKNearestNeighborSearch(queryPoint, k);
        }

        public IPoint[] FixedRadiusSearchForKNearestNeighbors(IPoint queryPoint,
                                                          float searchRadius,   // Search Radius bound
                                                          int k,                // number of near neighbors to return
                                                          float eps = 0)        // the error bound
        {
            float r = searchRadius * searchRadius;
            var list1 = this.points.Select(x => new KeyValuePair<float, IPoint>(PointDistanceSquared(queryPoint, x, this.dimension), x)).OrderBy(x => x.Key).Where(x => x.Key <= r).Select(x => x.Value).ToList();
            return list1.GetRange(0, Math.Min(k, list1.Count)).ToArray();
            //return this.points.Select(x => new KeyValuePair<float, IPoint>(PointDistanceSquared(queryPoint, x), x)).OrderBy(x => x.Key).Where(x => x.Key <= r).Select(x => x.Value).ToList().GetRange(0, k).ToArray();
        }

        private static float PointDistanceSquared(IPoint p1, IPoint p2, int dimension)
        {
            float sum = 0;
            for (int i = 0; i < dimension; i++)
            {
                float t = p1[i] - p2[i];
                sum += t * t;
            }

            return sum;
        }
    }
}
