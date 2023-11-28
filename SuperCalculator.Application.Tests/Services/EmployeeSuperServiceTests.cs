using Moq;
using SuperCalculator.Application.Builders;
using SuperCalculator.Application.Models;
using SuperCalculator.Application.Services;
using Xunit;

namespace SuperCalculator.Application.Tests.Services;

public class EmployeeSuperServiceTests
{
    [Fact]
    public async Task ProcessEmployeeSuperQuarterlyVariances_InvalidRawData()
    {
        // Arrange
        var rawDataReaderServiceMock = new Mock<IRawDataReaderService>();
        var summaryBuilderMock = new Mock<IEmployeeSuperQuarterlyVarianceSummaryBuilder>();

        var employeeSuperService = new EmployeeSuperService(rawDataReaderServiceMock.Object, summaryBuilderMock.Object);

        var fakeRawData = new byte[] { };

        rawDataReaderServiceMock.Setup(service => service.GetEmployeeSuperSummary(fakeRawData))
            .ReturnsAsync((RawEmployeeSuperData)null);

        // Act and Assert
        await Assert.ThrowsAsync<InvalidDataException>(
            async () => await employeeSuperService.ProcessEmployeeSuperQuarterlyVariances(fakeRawData));
    }
    
    [Fact]
    public async Task ProcessEmployeeSuperQuarterlyVariances_SuccessfulProcessing()
    {
        // Arrange
        var rawDataReaderServiceMock = new Mock<IRawDataReaderService>();
        var summaryBuilderMock = new Mock<IEmployeeSuperQuarterlyVarianceSummaryBuilder>();

        var employeeSuperService = new EmployeeSuperService(rawDataReaderServiceMock.Object, summaryBuilderMock.Object);

        var fakeRawData = new byte[] {  };
        var fakeRawSummary = new RawEmployeeSuperData(); 

        rawDataReaderServiceMock.Setup(service => service.GetEmployeeSuperSummary(fakeRawData))
            .ReturnsAsync(fakeRawSummary);

        // Act
        await employeeSuperService.ProcessEmployeeSuperQuarterlyVariances(fakeRawData);

        // Assert
        summaryBuilderMock.Verify(builder => builder.CreateEmployeeSuperQuarterlyVariances(fakeRawSummary), Times.Once);
    }
}