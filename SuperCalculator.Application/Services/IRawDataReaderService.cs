using SuperCalculator.Application.Models;

namespace SuperCalculator.Application.Services;

public interface IRawDataReaderService
{
    Task<RawEmployeeSuperData> GetEmployeeSuperSummary(byte[] rawData);
}