using SuperCalculator.Api.Models.Request;

namespace SuperCalculator.Api.Services;

public class RequestConverterService: IRequestConverterService
{
    public async Task<byte[]> GetRawDataProcessingRequest(EmployeeSuperQuarterlyVarianceRequest request)
    {
        if (request?.File != null)
        {
            await using var stream = request.File.OpenReadStream();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var file = ms.ToArray();
            return file;
        }

        throw new InvalidDataException("Request File in Null.");
    }
}