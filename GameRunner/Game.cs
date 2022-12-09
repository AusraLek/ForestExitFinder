using GameRunner.Data;
using GameRunner.Logic;

namespace GameRunner;

public class Game : IGame
{
    private readonly IMapReader mapReader;
    private readonly IExitFinder exitFinder;

    public Game(IMapReader mapReader, IExitFinder exitFinder)
    {
        this.mapReader = mapReader;
        this.exitFinder = exitFinder;
    }

    public int Run(string filePath)
    {
        var mapInfo = this.mapReader.ReadMap(filePath);

        if (mapInfo.MapCorrupted)
        {
            return 0;
        }

        var distance = this.exitFinder.FindDistance(mapInfo);

        return distance;
    }
}
