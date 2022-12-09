using FluentAssertions;
using GameRunner.Logic;
using GameRunner.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameRunner.Tests.Logic;

[TestClass]
public class ValidatorTests
{
    private readonly Validator validator;

    public ValidatorTests()
    {
        this.validator = new Validator();
    }

    [TestMethod]
    public void WhenFileExists()
    {
        // Act
        var result = this.validator.FileExists("TestData/TestFile.txt");

        // Assert
        result
            .Should()
            .BeTrue();
    }

    [TestMethod]
    public void WhenFileDoesNotExist()
    {
        // Act
        var result = this.validator.FileExists("TestData/TestFileNotExist.txt");

        // Assert
        result
            .Should()
            .BeFalse();
    }

    [TestMethod]
    public void WhenNoExitsInMap()
    {
        // Act
        var result = this.validator.HasExits(this.TestMapInfo);

        // Assert
        result
            .Should()
            .BeFalse();
    }

    [TestMethod]
    [DataRow(1, 0, DisplayName = "When top exit available")]
    [DataRow(1, 2, DisplayName = "When down exit available")]
    [DataRow(0, 1, DisplayName = "When left exit available")]
    [DataRow(2, 1, DisplayName = "When right exit available")]
    public void WhenExitIsAvailable(int x, int y)
    {
        // Arrange
        var mapInfo = this.TestMapInfo;
        mapInfo.Map[y][x] = false;

        // Act
        var result = this.validator.HasExits(mapInfo);

        // Assert
        result
            .Should()
            .BeTrue();
    }

    private MapInfo TestMapInfo
        => new MapInfo
        {
            LastX = 2,
            LastY = 2,
            Map = new List<List<bool>>
            {
                new List<bool> { true, true, true },
                new List<bool> { true, true, true },
                new List<bool> { true, true, true },
            }
        };
}
