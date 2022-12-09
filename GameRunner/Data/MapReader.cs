using GameRunner.Logic;
using GameRunner.Models;

namespace GameRunner.Data;

public class MapReader : IMapReader
{
    private const char TreeSymbol = '1';
    private const char EmptySpaceSymbol = ' ';
    private const char StartPositionSymbol = 'X';

    private readonly IValidator validator;

    public MapReader(IValidator validator)
    {
        this.validator = validator;
    }

    public MapInfo ReadMap(string filePath)
    {
        var mapInfo = new MapInfo();
        
        if (!this.validator.FileExists(filePath))
        {
            return this.FailedMapInfo;
        }

        var lines = File.ReadAllLines(filePath);

        var startingPointFound = false;

        mapInfo.LastY = lines.Length - 1;

        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            var mapLine = new List<bool>();
            var lineChars = line.ToCharArray();
            mapInfo.LastX = lineChars.Length - 1;

            for (var x = 0; x < lineChars.Length; x++)
            {
                var symbol = lineChars[x];

                if (symbol == TreeSymbol)
                {
                    mapLine.Add(true);
                }
                else if (symbol == EmptySpaceSymbol)
                {
                    mapLine.Add(false);
                }
                else if (symbol == StartPositionSymbol)
                {
                    if (startingPointFound)
                    {
                        return this.FailedMapInfo;
                    }

                    mapInfo.StartX = x;
                    mapInfo.StartY = y;
                    mapLine.Add(true);
                    startingPointFound = true;
                }
                else
                {
                    return this.FailedMapInfo;
                }
            }

            mapInfo.Map.Add(mapLine);
        }

        if (!this.validator.HasExits(mapInfo))
        {
            return this.FailedMapInfo;
        }

        return mapInfo;
    }

    private MapInfo FailedMapInfo
        => new MapInfo { MapCorrupted = true };
}
