using GameRunner.Models;

namespace GameRunner.Data;

public interface IMapReader
{
    MapInfo ReadMap(string filePath);
}
