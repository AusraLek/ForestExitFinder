using GameRunner.Models;

namespace GameRunner.Logic;

public class ExitFinder : IExitFinder
{
    public int FindDistance(MapInfo mapInfo)
    {
        var distance = 1;
        var coordinates = new List<Coordinates> { this.CreateNewCoordinates(mapInfo.StartX, mapInfo.StartY) };

        while (coordinates.Count > 0)
        {
            var nextCoordinates = new List<Coordinates>();

            foreach (var coord in coordinates)
            {
                mapInfo.Map[coord.Y][coord.X] = true;
                var nextMoves = this.GetValidMoves(coord, mapInfo);

                if(this.AnyMovesOnExit(mapInfo, nextMoves))
                {
                    return distance;
                }

                nextCoordinates.AddRange(nextMoves);
            }

            coordinates = nextCoordinates;
            distance++;
        }

        return 0;
    }

    private bool AnyMovesOnExit(MapInfo mapInfo, List<Coordinates> moves)
    {
        foreach (var move in moves)
        {
            if (IsCoordinatesAnExit(move, mapInfo))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsCoordinatesAnExit(Coordinates point, MapInfo mapInfo)
        => point.X == 0 || point.X == mapInfo.LastX || point.Y == 0 || point.Y == mapInfo.LastY;

    private List<Coordinates> GetValidMoves(Coordinates point, MapInfo mapInfo)
    {
        var validCoordinates = new List<Coordinates>();

        this.AddPointIfEmpty(point, mapInfo, validCoordinates, 0, -1);
        this.AddPointIfEmpty(point, mapInfo, validCoordinates, 0, 1);
        this.AddPointIfEmpty(point, mapInfo, validCoordinates, -1, 0);
        this.AddPointIfEmpty(point, mapInfo, validCoordinates, 1, 0);

        return validCoordinates;
    }

    private void AddPointIfEmpty(
        Coordinates point,
        MapInfo mapInfo,
        List<Coordinates> validCoordinates,
        int offsetX,
        int offsetY)
    {
        var newPoint = this.CreateNewCoordinates(point.X + offsetX, point.Y + offsetY);
        if (mapInfo.IsPointEmpty(newPoint.X, newPoint.Y))
        {
            validCoordinates.Add(newPoint);
        }
    }

    private Coordinates CreateNewCoordinates(int x, int y)
        => new Coordinates { X = x, Y = y };
}
