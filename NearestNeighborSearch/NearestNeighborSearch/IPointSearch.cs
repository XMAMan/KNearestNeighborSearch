using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NearestNeighborSearch
{
    public interface IPointSearch
    {
        IPoint[] ApproximateKNearestNeighborSearch(IPoint queryPoint,
                                                          int k,                // number of near neighbors to return
                                                          float eps = 0);        // the error bound

        IPoint[] PriorityKNearestNeighborSearch(IPoint queryPoint,
                                                          int k,                // number of near neighbors to return
                                                          float eps = 0);        // the error bound

        IPoint[] FixedRadiusSearchForKNearestNeighbors(IPoint queryPoint,
                                                          float searchRadius,   // Search Radius bound
                                                          int k,                // number of near neighbors to return
                                                          float eps = 0);        // the error bound
    }
}
