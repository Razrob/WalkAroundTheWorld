
public struct TerrainHeights
{
    public int Width;
    public int Height;

    private float[,] _heightsArray;

    public TerrainHeights(int _width, int _height)
    {
        Width = _width;
        Height = _height;
        _heightsArray = new float[Width, Height];
    }

    public float this[int x, int y]
    {
        get
        {
            return _heightsArray[x, y];
        }
        set
        {
            _heightsArray[x, y] = value;
        }
    }
}