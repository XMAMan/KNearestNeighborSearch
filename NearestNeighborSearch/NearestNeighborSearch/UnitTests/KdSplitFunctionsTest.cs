using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NearestNeighborSearch
{
    static class KdSplitFunctionsTest
    {
        public static string StartAllTests()
        {
            StringBuilder str = new StringBuilder();
            str.Append("MedianTest=" + MedianTest() + "\n");
            str.Append("MidpointTest=" + MidpointTest() + "\n");
            str.Append("SlidingMidpointTest=" + SlidingMidpointTest() + "\n");
            str.Append("FairSplitTest=" + FairSplitTest() + "\n");
            str.Append("SlidingFairSplitTest=" + SlidingFairSplitTest() + "\n");
            return str.ToString();
        }

        private static bool MedianTest()
        {
            return SplitTestUltra(KdSplitFunctions.MedianSplitFromLongestEdge, "[72, 81]\n[76, 55]\n[20, 55]\n[90, 44]\n[97, 27]\n[29, 46]\n[63, 46]\n[98, 3]\n[86, 99]\n[67, 31]\n----------------\n[98, 3]\n[97, 27]\n[67, 31]\n[90, 44]\n[29, 46]\n********\n[63, 46]\n[20, 55]\n[76, 55]\n[72, 81]\n[86, 99]\ncuttingDimension=1\ncuttingValue=46\n");
        }

        private static bool MidpointTest()
        {
            return SplitTestUltra(KdSplitFunctions.MidpointSplitFromLongestEdge, "[72, 81]\n[76, 55]\n[20, 55]\n[90, 44]\n[97, 27]\n[29, 46]\n[63, 46]\n[98, 3]\n[86, 99]\n[67, 31]\n----------------\n[67, 31]\n[98, 3]\n[63, 46]\n[90, 44]\n[97, 27]\n[29, 46]\n********\n[20, 55]\n[76, 55]\n[86, 99]\n[72, 81]\ncuttingDimension=1\ncuttingValue=51\n");
        }

        private static bool SlidingMidpointTest()
        {
            return SplitTestUltra(KdSplitFunctions.SlidingMidpointSplitFromLongestEdge, "[72, 81]\n[76, 55]\n[20, 55]\n[90, 44]\n[97, 27]\n[29, 46]\n[63, 46]\n[98, 3]\n[86, 99]\n[67, 31]\n----------------\n[67, 31]\n[98, 3]\n[63, 46]\n[90, 44]\n[97, 27]\n[29, 46]\n********\n[20, 55]\n[76, 55]\n[86, 99]\n[72, 81]\ncuttingDimension=1\ncuttingValue=51\n");
        }

        private static bool FairSplitTest()
        {
            return SplitTestUltra(KdSplitFunctions.FairSplit, "[72, 81]\n[76, 55]\n[20, 55]\n[90, 44]\n[97, 27]\n[29, 46]\n[63, 46]\n[98, 3]\n[86, 99]\n[67, 31]\n----------------\n[98, 3]\n[97, 27]\n[67, 31]\n[90, 44]\n[29, 46]\n********\n[63, 46]\n[20, 55]\n[76, 55]\n[72, 81]\n[86, 99]\ncuttingDimension=1\ncuttingValue=46\n");
        }

        private static bool SlidingFairSplitTest()
        {
            return SplitTestUltra(KdSplitFunctions.SlidingFairSplit, "[72, 81]\n[76, 55]\n[20, 55]\n[90, 44]\n[97, 27]\n[29, 46]\n[63, 46]\n[98, 3]\n[86, 99]\n[67, 31]\n----------------\n[98, 3]\n[97, 27]\n[67, 31]\n[90, 44]\n[29, 46]\n********\n[63, 46]\n[20, 55]\n[76, 55]\n[72, 81]\n[86, 99]\ncuttingDimension=1\ncuttingValue=46\n");
        }

        private static bool SplitTestUltra(KdSplitFunctions.SplittFunction splitFunctionHandler, string expectedResult)
        {
            int pointCount = 10;
            IPoint[] testData = new IPoint[pointCount];
            Random rand = new Random(0);
            for (int i = 0; i < testData.Length; i++)
            {
                testData[i] = new Point(rand.Next(100), rand.Next(100));
            }

            StringBuilder str = new StringBuilder();

            str.Append(TestAusgabe(testData));
            str.Append("\n----------------\n");
            var splitResult = splitFunctionHandler(testData, new AxisAlignedBox(testData, 2));
            str.Append(TestAusgabe(splitResult.PoingsOnLeftSide) + "\n********\n");
            str.Append(TestAusgabe(splitResult.PoingsOnRightSide) + "\n");
            str.Append("cuttingDimension=" + splitResult.CuttingDimension + "\n");
            str.Append("cuttingValue=" + splitResult.CuttingValue + "\n");

            string result = str.ToString();
            bool testOk = result == expectedResult;
            return testOk;
        }

        private static string TestAusgabe(IPoint[] testData)
        {
            return string.Join("\n", testData.ToList());
        }
    }
}
