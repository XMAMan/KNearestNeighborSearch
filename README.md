# KNearestNeighborSearch
## Search the K nearest points from a given set of points

I have translated the C++ Projekt from this site: https://www.cs.umd.edu/~mount/ANN/ into C# and have refactored it and made little bugfixes

It use a KD-Tree or a BoxDecomposition-Tree for the search

The usage looks like this:

//You have to implement this interface
public interface IPoint
{
    float this[int dimension] { get; }
}

//Example for a 3D-Point
class Point3D : IPoint
{
    public float[] data;

    public Point3D(float x, float y, float z)
    {
        this.data = new float[] { x, y, z };
    }

    public float this[int dimension]
    {
        get
        {
            return this.data[dimension];
        }
    }
}

//Search the 123-Nearest points around (50,50,50)
public static void Run()
{
    int pointCount = 100000;
    IPoint[] points = new IPoint[pointCount];
    Random rand = new Random(0);
    for (int i = 0; i < points.Length; i++)
    {
        points[i] = new Point3D(rand.Next(100), rand.Next(100), rand.Next(100));
    }

    KNearestNeighborSearch search = new KNearestNeighborSearch(points, 3); //3D-Points
    IPoint[] result = search.SearchKNearestNeighbors3D(50, 50, 50, 123); //Get the 123 nearest points
}
