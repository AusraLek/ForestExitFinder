using GameRunner.Models;

namespace GameRunner.Logic;

public interface IExitFinder
{
    int FindDistance(MapInfo mapInfo);
}
