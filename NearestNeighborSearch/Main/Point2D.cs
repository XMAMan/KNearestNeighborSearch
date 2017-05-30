using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NearestNeighborSearch;

namespace Main
{
    class Point2D : IPoint
    {
        public float[] data;

        public Point2D(float x, float y)
        {
            this.data = new float[] { x, y };
        }

        public float this[int dimension]
        {
            get
            {
                return this.data[dimension];
            }
        }
    }
}
