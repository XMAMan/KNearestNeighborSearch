using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NearestNeighborSearch
{
    interface IKdNode
    {
        bool IsEmptyLeaf();
        void ApproximateKNearestNeighborSearch(QueryAndResultDataForApproximateSearch data, float boxDistance);
        void PriorityKNearestNeighborSearch(QueryAndResultDataForPrioritySearch data, float boxDistance);
        void FixedRadiusSearchForKNearestNeighbors(QueryAndResultDataForFixedRadiusSearch data, float boxDistance);
    }

    class QueryAndResultDataForApproximateSearch
    {
        //Query-Data
        public IPoint QueryPoint;
        public float MaxTolerableSquaredError;

        //Result-Data
        public SortedArray<IPoint> KClosestPoints;
    }

    class QueryAndResultDataForPrioritySearch : QueryAndResultDataForApproximateSearch
    {
        public PriorityQueue<IKdNode> PriorityQueueForBoxes;
    }

    class QueryAndResultDataForFixedRadiusSearch : QueryAndResultDataForApproximateSearch
    {
        public float SquaredSearchRadius;
    }

    class KdSplitNode : IKdNode
    {
        private int cuttingDimension;
        private float cuttingValue;
        private float lowerBound, upperBound; //Grenze ist entlang der cuttingDimension
        private IKdNode leftChild, rightChild;

        public KdSplitNode(int cuttingDimension, float cuttingValue, float lowerBound, float upperBound, IKdNode leftChild, IKdNode rightChild)
        {
            this.cuttingDimension = cuttingDimension;
            this.cuttingValue = cuttingValue;
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
            this.leftChild = leftChild;
            this.rightChild = rightChild;
        }

        public bool IsEmptyLeaf()
        {
            return false;
        }

        //boxDistance = Shows the Distance(Shortest way) from the Query-Point to this Split-Node-Borders. 
        //It is the sum for every dimension which looks like this: a*a + b*b + c*c + d*d (a is Distance for Dimension 0; b is Distance for Dimension 1, c is Distance for Dimsion 2,..)
        public void ApproximateKNearestNeighborSearch(QueryAndResultDataForApproximateSearch data, float boxDistance)
        {
            float cutDiff = data.QueryPoint[this.cuttingDimension] - this.cuttingValue; // distance to cutting plane

            if (cutDiff < 0)    // left of cutting plane
            {
                this.leftChild.ApproximateKNearestNeighborSearch(data, boxDistance); // visit closer child first

                //boxDiff is needet, the remove the distance-component from this cuttingDimension from the boxDistance
                float boxDiff = this.lowerBound - data.QueryPoint[this.cuttingDimension]; 
                if (boxDiff < 0)    // within bounds - ignore                             
                    boxDiff = 0;

                boxDistance += cutDiff * cutDiff - boxDiff * boxDiff; //Transform boxDistance from Query-Point-To-This-Node to Qury-Point-To-RightChild

                // visit further child if close enough
                if (boxDistance * data.MaxTolerableSquaredError < data.KClosestPoints.GetMaximumKey())
                    this.rightChild.ApproximateKNearestNeighborSearch(data, boxDistance);
            }
            else  // right of cutting plane
            {
                this.rightChild.ApproximateKNearestNeighborSearch(data, boxDistance);    // visit closer child first

                float boxDiff = data.QueryPoint[this.cuttingDimension] - this.upperBound;
                if (boxDiff < 0)    // within bounds - ignore
                    boxDiff = 0;

                // distance to further box
                boxDistance += cutDiff * cutDiff - boxDiff * boxDiff;

                // visit further child if close enough
                if (boxDistance * data.MaxTolerableSquaredError < data.KClosestPoints.GetMaximumKey())
                    this.leftChild.ApproximateKNearestNeighborSearch(data, boxDistance);
            }
        }

        public void PriorityKNearestNeighborSearch(QueryAndResultDataForPrioritySearch data, float boxDistance)
        {
            float cutDiff = data.QueryPoint[this.cuttingDimension] - this.cuttingValue; // distance to cutting plane

            if (cutDiff < 0)    // left of cutting plane
            {
                float boxDiff = this.lowerBound - data.QueryPoint[this.cuttingDimension];
                if (boxDiff < 0)    // within bounds - ignore
                    boxDiff = 0;

                // distance to further box
                float newDistance = boxDistance + cutDiff * cutDiff - boxDiff * boxDiff;

                if (this.rightChild.IsEmptyLeaf() == false) // enqueue if not trivial
                    data.PriorityQueueForBoxes.Insert(newDistance, this.rightChild);

                this.leftChild.PriorityKNearestNeighborSearch(data, boxDistance);    // continue with closer child
            }
            else  // right of cutting plane
            {
                float boxDiff = data.QueryPoint[this.cuttingDimension] - this.upperBound;
                if (boxDiff < 0)    // within bounds - ignore
                    boxDiff = 0;

                // distance to further box
                float newDistance = boxDistance + cutDiff * cutDiff - boxDiff * boxDiff;

                if (this.leftChild.IsEmptyLeaf() == false) // enqueue if not trivial
                    data.PriorityQueueForBoxes.Insert(newDistance, this.leftChild);

                this.rightChild.PriorityKNearestNeighborSearch(data, boxDistance);    // continue with closer child
            }
        }

        public void FixedRadiusSearchForKNearestNeighbors(QueryAndResultDataForFixedRadiusSearch data, float boxDistance)
        {
            float cutDiff = data.QueryPoint[this.cuttingDimension] - this.cuttingValue; // distance to cutting plane

            if (cutDiff < 0)    // left of cutting plane
            {
                this.leftChild.FixedRadiusSearchForKNearestNeighbors(data, boxDistance); // visit closer child first

                float boxDiff = this.lowerBound - data.QueryPoint[this.cuttingDimension];
                if (boxDiff < 0)    // within bounds - ignore
                    boxDiff = 0;

                // distance to further box
                boxDistance += cutDiff * cutDiff - boxDiff * boxDiff;

                // visit further child if close enough
                if (boxDistance * data.MaxTolerableSquaredError < data.KClosestPoints.GetMaximumKey())
                    this.rightChild.FixedRadiusSearchForKNearestNeighbors(data, boxDistance);
            }
            else  // right of cutting plane
            {
                this.rightChild.FixedRadiusSearchForKNearestNeighbors(data, boxDistance);    // visit closer child first

                float boxDiff = data.QueryPoint[this.cuttingDimension] - this.upperBound;
                if (boxDiff < 0)    // within bounds - ignore
                    boxDiff = 0;

                // distance to further box
                boxDistance += cutDiff * cutDiff - boxDiff * boxDiff;

                // visit further child if close enough
                if (boxDistance * data.MaxTolerableSquaredError < data.KClosestPoints.GetMaximumKey())
                    this.leftChild.FixedRadiusSearchForKNearestNeighbors(data, boxDistance);
            }
        }
    }

    class KdLeafNode : IKdNode
    {
        private IPoint[] points;
        private int dimension;  //Every Point from the points-Array has the Dimensin 'dimension'. If 'points' contains 2D-Points, than dimension has the value 2

        public KdLeafNode(IPoint[] points, int dimension)
        {
            this.points = points;
            this.dimension = dimension;
        }

        public bool IsEmptyLeaf()
        {
            return this.points.Length == 0;
        }

        public void ApproximateKNearestNeighborSearch(QueryAndResultDataForApproximateSearch data, float boxDistance)
        {
            float minDist = data.KClosestPoints.GetMaximumKey();    // k-th smallest distance so far

            for (int i = 0; i < this.points.Length; i++)            // check points in bucket
            {
                IPoint pp = this.points[i];
                IPoint qq = data.QueryPoint;
                float distanceToDataPoint = 0;
                int d;

                for (d = 0; d < this.dimension; d++)
                {
                    float t = qq[d] - pp[d];
                    distanceToDataPoint += t * t;
                    if (distanceToDataPoint > minDist) break;       // exceeds dist to k-th smallest?
                }

                if (d >= this.dimension)                                 // among the k best?
                {
                    data.KClosestPoints.Insert(distanceToDataPoint, this.points[i]);
                    minDist = data.KClosestPoints.GetMaximumKey();
                }
            }
        }

        public void PriorityKNearestNeighborSearch(QueryAndResultDataForPrioritySearch data, float boxDistance)
        {
            ApproximateKNearestNeighborSearch(data, boxDistance);
        }

        public void FixedRadiusSearchForKNearestNeighbors(QueryAndResultDataForFixedRadiusSearch data, float boxDistance)
        {
            for (int i = 0; i < this.points.Length; i++)            // check points in bucket
            {
                IPoint pp = this.points[i];
                IPoint qq = data.QueryPoint;
                float distanceToDataPoint = 0;
                int d;

                for (d = 0; d < this.dimension; d++)
                {
                    float t = qq[d] - pp[d];
                    distanceToDataPoint += t * t;
                    if (distanceToDataPoint > data.SquaredSearchRadius) break;       // exceeds dist to k-th smallest?
                }

                if (d >= this.dimension)                                 // among the k best?
                {
                    data.KClosestPoints.Insert(distanceToDataPoint, this.points[i]);
                }
            }
        }
    }
}
