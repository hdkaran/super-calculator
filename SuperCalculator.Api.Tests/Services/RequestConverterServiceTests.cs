using Microsoft.AspNetCore.Http;
using SuperCalculator.Api.Models.Request;
using SuperCalculator.Api.Services;

namespace SuperCalculator.Api.Tests.Services;

public class RequestConverterServiceTests
{
    [Fact]
    public async Task GetRawDataProcessingRequest_ValidRequest()
    {
        // Arrange
        var requestConverterService = new RequestConverterService();
        var fakeFileContent = new byte[] { 0x01, 0x02, 0x03 }; // Replace with your actual file content
        var fakeFormFile = new FormFile(new MemoryStream(fakeFileContent), 0, fakeFileContent.Length, "file", "file.txt");

        var request = new EmployeeSuperQuarterlyVarianceRequest
        {
            File = fakeFormFile
        };

        // Act
        var result = await requestConverterService.GetRawDataProcessingRequest(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(fakeFileContent, result);
    }
    [Fact]
    public async Task GetRawDataProcessingRequest_InvalidRequest()
    {
        // Arrange
        var requestConverterService = new RequestConverterService();

        var request = new EmployeeSuperQuarterlyVarianceRequest
        {
            File = null
        };

        // Act and Assert
        await Assert.ThrowsAsync<InvalidDataException>(
            async () => await requestConverterService.GetRawDataProcessingRequest(request));
    }
}