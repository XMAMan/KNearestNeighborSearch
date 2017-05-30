using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NearestNeighborSearch
{
    class PriorityQueueTest
    {
        public static string RunAllTests()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Test1: " + Test1() + "\n");
            str.Append("Test2: " + Test2() + "\n");
            str.Append("Test3: " + Test3() + "\n");
            return str.ToString();
        }

        private static bool Test1()
        {
            PriorityQueue<int> q = new PriorityQueue<int>(5);
            return q.IsEmpty() && q.IsNonEmpty() == false;
        }

        private static bool Test2()
        {
            try
            {
                PriorityQueue<int> q = new PriorityQueue<int>(3);
                q.Insert(3, 3);
                q.Insert(7, 7);
                q.Insert(2, 2);
                q.Insert(8, 8);
                q.Insert(1, 1);
                return false; //Ich erwarte eine Exception, da 5 eingefügte Elemente mehr als 3 sind
            }
            catch (Exception ex)
            {
                return ex.Message == "Priority queue overflow.";
            }
        }

        private static bool Test3()
        {
            PriorityQueue<int> q = new PriorityQueue<int>(5);
            q.Insert(3, 3);
            q.Insert(7, 7);
            q.Insert(2, 2);
            q.Insert(8, 8);
            q.Insert(1, 1);
            bool b1 = q.ExtractMinimum().Key == 1.0f;
            bool b2 = q.ExtractMinimum().Key == 2.0f;
            bool b3 = q.ExtractMinimum().Key == 3.0f;
            bool b4 = q.ExtractMinimum().Key == 7.0f;
            bool b5 = q.ExtractMinimum().Key == 8.0f;
            bool b6 = q.IsEmpty();
            return b1 && b2 && b3 && b4 && b5 && b6;
        }
    }
}
