using FluentAssertions;
using GameRunner.Data;
using GameRunner.Logic;
using GameRunner.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace GameRunner.Tests.Data;

[TestClass]
public class MapReaderTests
{
    private readonly Mock<IValidator> validator;
    private readonly MapReader mapReader;

    public MapReaderTests()
    {
        this.validator = new Mock<IValidator>();
        this.mapReader = new MapReader(this.validator.Object);
    }

    [TestMethod]
    public void ReadMapWhenFileNotExists()
    {
        // Arrange
        this.validator
            .Setup(mock => mock.FileExists(It.IsAny<string>()))
            .Returns(false);

        // Act
        var result = this.mapReader.ReadMap("Test");

        // Assert
        result
            .MapCorrupted
            .Should()
            .BeTrue();
    }

    [TestMethod]
    [DataRow("TestData/InvalidSymbol.txt", DisplayName = "Invalid symbol - 2")]
    [DataRow("TestData/InvalidSymbol_LowerX.txt", DisplayName = "Invalid symbol - lower x")]
    [DataRow("TestData/InvalidSymbol_MultipleX.txt", DisplayName = "Invalid symbol - multiple X")]
    public void ReadMapWhenInvalidSymbol(string path)
    {
        // Arrange
        this.validator
            .Setup(mock => mock.FileExists(It.IsAny<string>()))
            .Returns(true);

        // Act
        var result = this.mapReader.ReadMap(path);

        // Assert
        result
            .MapCorrupted
            .Should()
            .BeTrue();
    }

    [TestMethod]
    public void ReadMapWhenNoExits()
    {
        // Arrange
        this.validator
            .Setup(mock => mock.FileExists(It.IsAny<string>()))
            .Returns(true);
        this.validator
            .Setup(mock => mock.HasExits(It.IsAny<MapInfo>()))
            .Returns(false);

        // Act
        var result = this.mapReader.ReadMap("TestData/TestMap.txt");

        // Assert
        result
            .MapCorrupted
            .Should()
            .BeTrue();
    }

    [TestMethod]
    public void ReadMapWithInitialSample()
    {
        // Arrange
        this.validator
            .Setup(mock => mock.FileExists(It.IsAny<string>()))
            .Returns(true);
        this.validator
            .Setup(mock => mock.HasExits(It.IsAny<MapInfo>()))
            .Returns(true);

        // Act
        var result = this.mapReader.ReadMap("TestData/TestMapInitial.txt");

        // Assert
        result
            .Should()
            .BeEquivalentTo(new MapInfo
            {
                MapCorrupted = false,
                StartX = 2,
                StartY = 1,
                LastX = 4,
                LastY = 4,
                Map = new List<List<bool>>
                {
                    new List<bool> { true, true, true, true, true },
                    new List<bool> { true, false, true, false, true },
                    new List<bool> { true, false, true, false, true },
                    new List<bool> { true, false, false, false, true },
                    new List<bool> { true, false, true, true, true },
                }
            });
    }

    [TestMethod]
    public void ReadMapWithCustomSample()
    {
        // Arrange
        this.validator
            .Setup(mock => mock.FileExists(It.IsAny<string>()))
            .Returns(true);
        this.validator
            .Setup(mock => mock.HasExits(It.IsAny<MapInfo>()))
            .Returns(true);

        // Act
        var result = this.mapReader.ReadMap("TestData/TestMapCustom.txt");

        // Assert
        result
            .Should()
            .BeEquivalentTo(new MapInfo
            {
                MapCorrupted = false,
                StartX = 1,
                StartY = 1,
                LastX = 10,
                LastY = 4,
                Map = new List<List<bool>>
                {
                    new List<bool> { true, true, true, true, true, true, true, true, true, true, true },
                    new List<bool> { true, true, false, false, false, false, true, false, false, false, true },
                    new List<bool> { true, false, true, false, true, false, true, true, true, false, true },
                    new List<bool> { true, false, true, true, true, false, false, false, false, false, true },
                    new List<bool> { true, true, true, false, true, false, true, true, true, true, true },
                }
            });
    }
}
