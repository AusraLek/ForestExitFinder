using FluentAssertions;
using GameRunner.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameRunner.Tests.Models;

[TestClass]
public class MapInfoTests
{
    [TestMethod]
    [DataRow(0, 0, false)]
    [DataRow(1, 1, false)]
    [DataRow(0, 1, true)]
    [DataRow(1, 2, false)]
    [DataRow(2, 2, true)]
    public void ReturnsCorrectIsPointEmpty(int x, int y, bool expectedResult)
    {
        // Arrange
        var map = new MapInfo
        {
            LastX = 2,
            LastY = 2,
            Map = new List<List<bool>>
            {
                new List<bool> { true, true, true },
                new List<bool> { false, true, true },
                new List<bool> { true, true, false },
            }
        };

        // Act
        var result = map.IsPointEmpty(x, y);

        // Assert
        result
            .Should()
            .Be(expectedResult);
    }
}
