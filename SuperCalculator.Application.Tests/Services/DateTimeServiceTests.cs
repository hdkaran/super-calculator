using SuperCalculator.Application.Services;

namespace SuperCalculator.Application.Tests.Services;

using System;
using System.Collections.Generic;
using Xunit;

public class DateTimeServiceTests
{
    [Fact]
    public void GetQuartersForDates_ReturnsCorrectQuarters()
    {
        // Arrange
        var dateTimeService = new DateTimeService();
        var dates = new List<DateTime>
        {
            new DateTime(2023, 1, 15),
            new DateTime(2023, 5, 20),
            new DateTime(2023, 8, 10),
            new DateTime(2023, 12, 5)
        };

        // Act
        var quarters = dateTimeService.GetQuartersForDates(dates);

        // Assert
        Assert.NotNull(quarters);
        Assert.Equal(4, quarters.Count);

        // Verify each quarter corresponds to the expected date range
        Assert.Equal(new DateTime(2023, 1, 1), quarters[0].From);
        Assert.Equal(new DateTime(2023, 3, 31), quarters[0].To);

        Assert.Equal(new DateTime(2023, 4, 1), quarters[1].From);
        Assert.Equal(new DateTime(2023, 6, 30), quarters[1].To);

        Assert.Equal(new DateTime(2023, 7, 1), quarters[2].From);
        Assert.Equal(new DateTime(2023, 9, 30), quarters[2].To);

        Assert.Equal(new DateTime(2023, 10, 1), quarters[3].From);
        Assert.Equal(new DateTime(2023, 12, 31), quarters[3].To);
    }

    [Fact]
    public void GetQuartersForDates_EmptyList_ThrowsException()
    {
        // Arrange
        var dateTimeService = new DateTimeService();
        var emptyDates = new List<DateTime>();

        // Act and Assert
        var exception = Assert.Throws<InvalidOperationException>(
            () => dateTimeService.GetQuartersForDates(emptyDates));

        Assert.Equal("Cannot get quarters for an empty or null date list", exception.Message);
    }
}
