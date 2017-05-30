using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NearestNeighborSearch
{
    //Hilfsklasse, um das Interface 'IPointSearch' zu testen
    static class PointSearchTestHelper
    {
        public static List<PointSearchMethod> GetAllPossiblePointSearchMethods(IPoint[] points, int dimension)
        {
            return new List<PointSearchMethod>()
            {
                new PointSearchMethod("PointSearchBruthForce", new PointSearchBruthForce(points, dimension)),
                new PointSearchMethod("KdTree-Fair", new KdTree(points, dimension, KdTree.SplitRule.Fair)),
                new PointSearchMethod("KdTree-Midpoint", new KdTree(points, dimension, KdTree.SplitRule.Midpoint)),
                new PointSearchMethod("KdTree-SlidingFair", new KdTree(points, dimension, KdTree.SplitRule.SlidingFair)),
                new PointSearchMethod("KdTree-SlidingMidpoint", new KdTree(points, dimension, KdTree.SplitRule.SlidingMidpoint)),
                new PointSearchMethod("KdTree-Standard", new KdTree(points, dimension, KdTree.SplitRule.Standard)),
                new PointSearchMethod("BdTree-Fair-CentroidSplitting", new BdTree(points, dimension, KdTree.SplitRule.Fair, BdTree.ShrinkRule.CentroidSplitting)),
                new PointSearchMethod("BdTree-Midpoint-CentroidSplitting", new BdTree(points, dimension, KdTree.SplitRule.Midpoint, BdTree.ShrinkRule.CentroidSplitting)),
                new PointSearchMethod("BdTree-SlidingFair-CentroidSplitting", new BdTree(points, dimension, KdTree.SplitRule.SlidingFair, BdTree.ShrinkRule.CentroidSplitting)),
                new PointSearchMethod("BdTree-SlidingMidpoint-CentroidSplitting", new BdTree(points, dimension, KdTree.SplitRule.SlidingMidpoint, BdTree.ShrinkRule.CentroidSplitting)),
                new PointSearchMethod("BdTree-Standard-CentroidSplitting", new BdTree(points, dimension, KdTree.SplitRule.Standard, BdTree.ShrinkRule.CentroidSplitting)),
                new PointSearchMethod("BdTree-Fair-SimpleSplitting", new BdTree(points, dimension, KdTree.SplitRule.Fair, BdTree.ShrinkRule.SimpleSplitting)),
                new PointSearchMethod("BdTree-Midpoint-SimpleSplitting", new BdTree(points, dimension, KdTree.SplitRule.Midpoint, BdTree.ShrinkRule.SimpleSplitting)),
                new PointSearchMethod("BdTree-SlidingFair-SimpleSplitting", new BdTree(points, dimension, KdTree.SplitRule.SlidingFair, BdTree.ShrinkRule.SimpleSplitting)),
                new PointSearchMethod("BdTree-SlidingMidpoint-SimpleSplitting", new BdTree(points, dimension, KdTree.SplitRule.SlidingMidpoint, BdTree.ShrinkRule.SimpleSplitting)),
                new PointSearchMethod("BdTree-Standard-SimpleSplitting", new BdTree(points, dimension, KdTree.SplitRule.Standard, BdTree.ShrinkRule.SimpleSplitting)),
            };
        }

        public static IPoint[] CreatePointArray(Random rand, int pointCount, int[] bounds)
        {
            List<IPoint> points = new List<IPoint>();

            for (int i = 0; i < pointCount; i++)
            {
                points.Add(CreatePoint(rand, bounds));
            }

            return points.ToArray();
        }

        public static IPoint CreatePoint(Random rand, int[] bounds)
        {
            List<float> val = new List<float>();
            for (int d = 0; d < bounds.Length; d++)
            {
                val.Add((float)(rand.NextDouble() * bounds[d]));
            }
            return new Point(val.ToArray());
        }
    }

    class PointSearchMethod
    {
        public string Name;
        public IPointSearch Search;

        public PointSearchMethod(string name, IPointSearch search)
        {
            this.Name = name;
            this.Search = search;
        }
    }
}
