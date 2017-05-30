using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NearestNeighborSearch
{
    //Beim Geschwindigkeitstest bin ich zu folgenden Ergebnis gegkommen
    //Bei allen 3 Suchverfahren (ApproximateKNearestNeighborSearch, PriorityKNearestNeighborSearch, FixedRadiusSearchForKNearestNeighbors) im 2D und 3D-Suchfeld 
    //-> Platz 1: BdTree-SlidingMidpoint-SimpleSplitting, Platz 2: BdTree-Standard-SimpleSplitting

    //Will ich in Zukunft eine K-Nearest-Neighbor-Suche machen, dann sollte ich ApproximateKNearestNeighborSearch mit BdTree-Standard-SimpleSplitting nutzen

    //2D-Suchfeld (So viele Millisekunden hat die Suche gedauert); Suchpointcount (k) = 10
    //ApproximateKNearestNeighborSearch
    //PointSearchBruthForce	KdTree-Fair	KdTree-Midpoint	KdTree-SlidingFair	KdTree-SlidingMidpoint	KdTree-Standard	BdTree-Fair-CentroidSplitting	BdTree-Midpoint-CentroidSplitting	BdTree-SlidingFair-CentroidSplitting	BdTree-SlidingMidpoint-CentroidSplitting	BdTree-Standard-CentroidSplitting	BdTree-Fair-SimpleSplitting	BdTree-Midpoint-SimpleSplitting	BdTree-SlidingFair-SimpleSplitting	BdTree-SlidingMidpoint-SimpleSplitting	BdTree-Standard-SimpleSplitting
    //197	15	4	9	5	3	3	20	2	19	3	2	2	3	2	2   -> 1000 Daten-Punkte
    //366	3	3	3	2	3	3	75	2	72	2	3	2	3	2	3   -> 5000 Daten-Punkte
    //845	3	2	3	3	2	3	194	2	168	3	2	2	3	3	2   -> 10000 Daten-Punkte

    //PriorityKNearestNeighborSearch
    //PointSearchBruthForce	KdTree-Fair	KdTree-Midpoint	KdTree-SlidingFair	KdTree-SlidingMidpoint	KdTree-Standard	BdTree-Fair-CentroidSplitting	BdTree-Midpoint-CentroidSplitting	BdTree-SlidingFair-CentroidSplitting	BdTree-SlidingMidpoint-CentroidSplitting	BdTree-Standard-CentroidSplitting	BdTree-Fair-SimpleSplitting	BdTree-Midpoint-SimpleSplitting	BdTree-SlidingFair-SimpleSplitting	BdTree-SlidingMidpoint-SimpleSplitting	BdTree-Standard-SimpleSplitting
    //66	6	8	3	5	3	3	6	3	4	3	3	2	3	3	3
    //406	4	5	4	5	3	4	7	3	6	4	9	4	4	6	5
    //909	6	7	7	5	5	4	10	4	8	4	5	5	4	5	5

    //FixedRadiusSearchForKNearestNeighbors
    //PointSearchBruthForce	KdTree-Fair	KdTree-Midpoint	KdTree-SlidingFair	KdTree-SlidingMidpoint	KdTree-Standard	BdTree-Fair-CentroidSplitting	BdTree-Midpoint-CentroidSplitting	BdTree-SlidingFair-CentroidSplitting	BdTree-SlidingMidpoint-CentroidSplitting	BdTree-Standard-CentroidSplitting	BdTree-Fair-SimpleSplitting	BdTree-Midpoint-SimpleSplitting	BdTree-SlidingFair-SimpleSplitting	BdTree-SlidingMidpoint-SimpleSplitting	BdTree-Standard-SimpleSplitting
    //72	43	48	49	40	36	24	42	33	36	23	29	33	32	34	24
    //403	15	18	13	13	12	11	76	11	75	10	10	12	11	11	10
    //780	4	3	4	3	2	3	196	5	188	3	5	3	3	5	2

    //3D-Suchfeld
    //ApproximateKNearestNeighborSearch
    //PointSearchBruthForce	KdTree-Fair	KdTree-Midpoint	KdTree-SlidingFair	KdTree-SlidingMidpoint	KdTree-Standard	BdTree-Fair-CentroidSplitting	BdTree-Midpoint-CentroidSplitting	BdTree-SlidingFair-CentroidSplitting	BdTree-SlidingMidpoint-CentroidSplitting	BdTree-Standard-CentroidSplitting	BdTree-Fair-SimpleSplitting	BdTree-Midpoint-SimpleSplitting	BdTree-SlidingFair-SimpleSplitting	BdTree-SlidingMidpoint-SimpleSplitting	BdTree-Standard-SimpleSplitting
    //101	14	11	4	3	4	4	23	3	22	4	3	4	4	4	3
    //439	4	5	5	4	5	7	119	5	93	3	4	5	4	3	4
    //892	4	5	5	4	4	5	233	4	214	5	4	4	5	5	5

    //PriorityKNearestNeighborSearch
    //PointSearchBruthForce	KdTree-Fair	KdTree-Midpoint	KdTree-SlidingFair	KdTree-SlidingMidpoint	KdTree-Standard	BdTree-Fair-CentroidSplitting	BdTree-Midpoint-CentroidSplitting	BdTree-SlidingFair-CentroidSplitting	BdTree-SlidingMidpoint-CentroidSplitting	BdTree-Standard-CentroidSplitting	BdTree-Fair-SimpleSplitting	BdTree-Midpoint-SimpleSplitting	BdTree-SlidingFair-SimpleSplitting	BdTree-SlidingMidpoint-SimpleSplitting	BdTree-Standard-SimpleSplitting
    //75	14	4	5	7	5	5	9	5	7	5	4	5	4	6	4
    //437	7	8	6	6	7	5	12	5	13	5	6	6	5	7	7
    //992	10	7	8	8	8	8	14	9	11	7	8	8	7	8	8

    //FixedRadiusSearchForKNearestNeighbors
    //PointSearchBruthForce	KdTree-Fair	KdTree-Midpoint	KdTree-SlidingFair	KdTree-SlidingMidpoint	KdTree-Standard	BdTree-Fair-CentroidSplitting	BdTree-Midpoint-CentroidSplitting	BdTree-SlidingFair-CentroidSplitting	BdTree-SlidingMidpoint-CentroidSplitting	BdTree-Standard-CentroidSplitting	BdTree-Fair-SimpleSplitting	BdTree-Midpoint-SimpleSplitting	BdTree-SlidingFair-SimpleSplitting	BdTree-SlidingMidpoint-SimpleSplitting	BdTree-Standard-SimpleSplitting
    //74	32	38	27	28	29	20	30	18	27	19	21	26	22	28	21
    //428	146	188	148	141	143	105	150	104	135	99	120	130	119	127	115
    //879	302	388	290	290	285	218	294	220	271	203	240	281	241	270	237

    class PointSearchTest
    {
        public static string TestAllMethods(int[] bounds, int[] dataPointCounts, int queryPointCount, int searchPointCount, float searchRadius, float eps = 0)
        {
            StringBuilder str = new StringBuilder();

            string s1 = TestAllMethods("ApproximateKNearestNeighborSearch", bounds, dataPointCounts, queryPointCount, (s, q) => { return s.ApproximateKNearestNeighborSearch(q, searchPointCount, eps); }, str);
            string s2 = TestAllMethods("PriorityKNearestNeighborSearch", bounds, dataPointCounts, queryPointCount, (s, q) => { return s.PriorityKNearestNeighborSearch(q, searchPointCount, eps); }, str);
            string s3 = TestAllMethods("FixedRadiusSearchForKNearestNeighbors", bounds, dataPointCounts, queryPointCount, (s, q) => { return s.FixedRadiusSearchForKNearestNeighbors(q, searchRadius, searchPointCount, eps); }, str);

            return s1 + "\n" + s2 + "\n" + s3 + "\n\n\n" + str.ToString();
        }

        private static string TestAllMethods(string searchName, int[] bounds, int[] dataPointCounts, int queryPointCount, Func<IPointSearch, IPoint, IPoint[]> func, StringBuilder strTest)
        {
            StringBuilder str = new StringBuilder();
            str.Append(searchName + "\n");
            str.Append(string.Join("\t", GetAllMethodNames().ToList()) + "\n");
            for (int i = 0; i < dataPointCounts.Length; i++)
            {
                var times = TestAllMethods(i, bounds, dataPointCounts[i], queryPointCount, func, strTest);
                str.Append(string.Join("\t", times.ToList()) + "\n");
            }
            return str.ToString();
        }

        private static string[] GetAllMethodNames()
        {
            return PointSearchTestHelper.GetAllPossiblePointSearchMethods(new IPoint[] { new Point(0.0f) }, 1).Select(x => x.Name).ToArray();
        }

        private static int[] TestAllMethods(int seed, int[] bounds, int dataPointCount, int queryPointCount, Func<IPointSearch, IPoint, IPoint[]> func, StringBuilder str)
        {
            Random rand = new Random(seed);
            var dataPoints = PointSearchTestHelper.CreatePointArray(rand, dataPointCount, bounds);
            var queryPoints = PointSearchTestHelper.CreatePointArray(rand, queryPointCount, bounds);
            var searchMethods = PointSearchTestHelper.GetAllPossiblePointSearchMethods(dataPoints, bounds.Length);

            return TestAllMethods(searchMethods, queryPoints, func, str);
        }

        private static int[] TestAllMethods(List<PointSearchMethod> searchMethods, IPoint[] queryPoints, Func<IPointSearch, IPoint, IPoint[]> func, StringBuilder str)
        {
            List<int> timesInMs = new List<int>();
            foreach (var method in searchMethods)
            {
                str.Append(method.Name + "\n");
                int timeInMs = TestSingleMethod(method, queryPoints, func, str);
                timesInMs.Add(timeInMs);
            }
            return timesInMs.ToArray();
        }

        private static int TestSingleMethod(PointSearchMethod method, IPoint[] queryPoints, Func<IPointSearch, IPoint, IPoint[]> func, StringBuilder str)
        {
            DateTime startTime = DateTime.Now;
            for (int i = 0; i < queryPoints.Length; i++)
            {
                var result = func(method.Search, queryPoints[i]);
                str.Append(i + ":" + string.Join(", ", result.ToList()) + "\n");
            }
            DateTime endTime = DateTime.Now;
            return (int)(endTime - startTime).TotalMilliseconds;
        }
    }
}
