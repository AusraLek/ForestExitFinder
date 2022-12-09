using GameRunner.Models;

namespace GameRunner.Logic;

public interface IValidator
{
    bool FileExists(string filePath);
    bool HasExits(MapInfo mapInfo);
}
