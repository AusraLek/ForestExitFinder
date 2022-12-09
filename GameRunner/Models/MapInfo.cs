namespace GameRunner.Models;

public class MapInfo
{
    public MapInfo()
    {
        this.Map = new List<List<bool>>();
    }
    public List<List<bool>> Map { get; set; }
    public int StartX { get; set; }
    public int StartY { get; set; }
    public int LastX { get; set; }
    public int LastY { get; set; }
    public bool MapCorrupted { get; set; }

    public bool IsPointEmpty(int x, int y)
        => !this.Map[y][x];
}
