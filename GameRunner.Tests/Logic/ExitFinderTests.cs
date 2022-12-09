using FluentAssertions;
using GameRunner.Logic;
using GameRunner.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameRunner.Tests.Logic;

[TestClass]
public class ExitFinderTests
{
    private readonly ExitFinder exitFinder;

    public ExitFinderTests()
    {
        this.exitFinder = new ExitFinder();
    }

    [TestMethod]
    public void FindDistanceWhenExitIsReachable()
    {
        // Arrange
        var map = new MapInfo
        {
            MapCorrupted = false,
            StartX = 1,
            StartY = 1,
            LastX = 4,
            LastY = 4,
            Map = new List<List<bool>>
            {
                new List<bool> { true, true, true, true, true },
                new List<bool> { true, true, false, false, true },
                new List<bool> { true, false, true, false, false },
                new List<bool> { true, false, true, false, true },
                new List<bool> { true, true, true, true, true },
            }
        };

        // Act
        var result = this.exitFinder.FindDistance(map);

        // Assert
        result
            .Should()
            .Be(4);
    }

    [TestMethod]
    public void FindDistanceWhenExitIsNotReachable()
    {
        // Arrange
        var map = new MapInfo
        {
            MapCorrupted = false,
            StartX = 1,
            StartY = 1,
            LastX = 4,
            LastY = 4,
            Map = new List<List<bool>>
            {
                new List<bool> { true, true, true, true, true },
                new List<bool> { true, true, false, false, true },
                new List<bool> { true, false, true, false, true },
                new List<bool> { true, false, true, false, true },
                new List<bool> { true, true, true, true, true },
            }
        };

        // Act
        var result = this.exitFinder.FindDistance(map);

        // Assert
        result
            .Should()
            .Be(0);
    }
}
