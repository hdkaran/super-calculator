using Moq;
using SuperCalculator.Application.Builders;
using SuperCalculator.Application.Models;
using SuperCalculator.Application.Services;
using SuperCalculator.Application.Tests.DataGenerator;
using Xunit;

namespace SuperCalculator.Application.Tests.Builders;

public class EmployeeSuperQuarterlyVarianceSummaryBuilderTests
{
    private RawEmployeeSuperData CreateSampleRawEmployeeSuperData()
    {
        return TestDataGenerator.CreateSampleRawEmployeeSuperData();
    }
    
    [Fact]
    public void CreateEmployeeSuperQuarterlyVariances_ReturnsValidSummary()
    {
        // Arrange
        var builder = new EmployeeSuperQuarterlyVarianceSummaryBuilder(new DateTimeService());
        var rawSummary = CreateSampleRawEmployeeSuperData();
        

        // Act
        var result = builder.CreateEmployeeSuperQuarterlyVariances(rawSummary);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.EmployeeSuperQuarterlyVariances);
        Assert.True(result.EmployeeSuperQuarterlyVariances.Any()); 
    }
}