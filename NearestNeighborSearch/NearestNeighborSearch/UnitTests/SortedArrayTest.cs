using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NearestNeighborSearch
{
    class SortedArrayTest
    {
        public static string RunAllTests()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Test1: " + Test1() + "\n");
            str.Append("Test2: " + Test2() + "\n");
            str.Append("Test3: " + Test3() + "\n");
            str.Append("Test4: " + Test4() + "\n");
            str.Append("Test5: " + Test5() + "\n");
            str.Append("Test6: " + Test6() + "\n");
            return str.ToString();
        }

        private static bool Test1()
        {
            SortedArray<int> q = new SortedArray<int>(5);
            return q.GetMinimumKey() == float.MaxValue;
        }

        private static bool Test2()
        {
            SortedArray<int> q = new SortedArray<int>(3);
            q.Insert(3, 3);
            q.Insert(7, 7);
            q.Insert(2, 2);
            q.Insert(8, 8);
            q.Insert(1, 1);
            return q.GetMinimumKey() == 1.0f;
        }

        private static bool Test3()
        {
            SortedArray<int> q = new SortedArray<int>(5);
            return q.GetMaximumKey() == float.MaxValue;
        }

        private static bool Test4()
        {
            SortedArray<int> q = new SortedArray<int>(3);
            q.Insert(3, 3);
            q.Insert(7, 7);
            q.Insert(2, 2);
            q.Insert(8, 8);
            q.Insert(1, 1);
            return q.GetMaximumKey() == 3.0f;
        }

        private static bool Test5()
        {
            SortedArray<int> q = new SortedArray<int>(5);
            q.Insert(3, 3);
            q.Insert(7, 7);
            return q.GetMaximumKey() == 7.0f;
        }

        private static bool Test6()
        {
            SortedArray<int> q = new SortedArray<int>(3);
            q.Insert(3, 3);
            q.Insert(7, 7);
            q.Insert(2, 2);
            q.Insert(8, 8);
            q.Insert(1, 1);
            return q[0].Value == 1 &&
                   q[1].Value == 2 &&
                   q[2].Value == 3 &&
                   q[4].Key == float.MaxValue &&
                   q[4].Value == default(int);
        }
    }
}
