using SuperCalculator.Api.Models.Request;

namespace SuperCalculator.Api.Services;

public interface IRequestConverterService
{
    Task<byte[]> GetRawDataProcessingRequest(
        EmployeeSuperQuarterlyVarianceRequest request);
}