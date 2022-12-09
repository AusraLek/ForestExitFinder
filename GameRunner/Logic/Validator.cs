using GameRunner.Models;

namespace GameRunner.Logic;

public class Validator : IValidator
{
    public bool FileExists(string filePath)
        => File.Exists(filePath);

    public bool HasExits(MapInfo mapInfo)
    {
        for (var x = 0; x <= mapInfo.LastX; x++)
        {
            if (mapInfo.IsPointEmpty(x, 0) || mapInfo.IsPointEmpty(x, mapInfo.LastY))
            {
                return true;
            }
        }

        for (var y = 0; y <= mapInfo.LastY; y++)
        {
            if (mapInfo.IsPointEmpty(0, y) || mapInfo.IsPointEmpty(mapInfo.LastX, y))
            {
                return true;
            }
        }

        return false;
    }
}
