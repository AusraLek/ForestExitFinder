using FluentAssertions;
using GameRunner.Data;
using GameRunner.Logic;
using GameRunner.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameRunner.Tests;

[TestClass]
public class GameTests
{
    private readonly Game game;
    private readonly Mock<IMapReader> mapReader;
    private readonly Mock<IExitFinder> exitFinder;

    public GameTests()
    {
        this.mapReader = new Mock<IMapReader>();
        this.exitFinder = new Mock<IExitFinder>();
        this.game = new Game(this.mapReader.Object, this.exitFinder.Object);
    }

    [TestMethod]
    public void RunWhenMapIsCorrupted()
    {
        // Arrange
        var corruptedMap = new MapInfo { MapCorrupted = true };
        this.mapReader
            .Setup(mock => mock.ReadMap(It.IsAny<string>()))
            .Returns(corruptedMap);

        // Act
        var result = this.game.Run("Test");

        // Assert
        result
            .Should()
            .Be(0);
    }

    [TestMethod]
    [DataRow(5)]
    [DataRow(0)]
    [DataRow(12345)]
    public void RunWhenWhenEverythingIsOk(int expectedDistance)
    {
        // Arrange
        var map = new MapInfo();
        this.mapReader
            .Setup(mock => mock.ReadMap(It.IsAny<string>()))
            .Returns(map);

        this.exitFinder
            .Setup(mock => mock.FindDistance(It.IsAny<MapInfo>()))
            .Returns(expectedDistance);

        // Act
        var result = this.game.Run("Test");

        // Assert
        result
            .Should()
            .Be(expectedDistance);
    }
}
