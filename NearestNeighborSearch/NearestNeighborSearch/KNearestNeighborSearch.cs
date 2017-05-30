using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NearestNeighborSearch
{
    public class KNearestNeighborSearch
    {
        private IPointSearch pointSearch;

        public KNearestNeighborSearch(IPoint[] points, int dimension)
        {
            this.pointSearch = new BdTree(points, dimension);
        }

        public IPoint[] SearchKNearestNeighbors2D(float x, float y, int k)
        {
            return this.pointSearch.ApproximateKNearestNeighborSearch(new Point(x, y), k); 
        }

        public IPoint[] SearchKNearestNeighbors3D(float x, float y, float z, int k)
        {
            return this.pointSearch.ApproximateKNearestNeighborSearch(new Point(x, y, z), k);
        }
    }
}
