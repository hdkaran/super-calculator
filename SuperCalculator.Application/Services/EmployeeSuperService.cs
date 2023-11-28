using SuperCalculator.Application.Builders;
using SuperCalculator.Application.Models;

namespace SuperCalculator.Application.Services;

public class EmployeeSuperService: IEmployeeSuperService
{
    private readonly IRawDataReaderService _rawDataReaderService;
    private readonly IEmployeeSuperQuarterlyVarianceSummaryBuilder _summaryBuilder;

    public EmployeeSuperService(IRawDataReaderService rawDataReaderService, IEmployeeSuperQuarterlyVarianceSummaryBuilder summaryBuilder)
    {
        _rawDataReaderService = rawDataReaderService;
        _summaryBuilder = summaryBuilder;
    }
    public async Task<EmployeeSuperQuarterlyVarianceSummary> ProcessEmployeeSuperQuarterlyVariances(byte[] rawData)
    {
        var rawSummary = await _rawDataReaderService.GetEmployeeSuperSummary(rawData);
        
        if (rawSummary == null)
        {
            throw new InvalidDataException("Unable to process the raw data.");
        }
        
        return _summaryBuilder.CreateEmployeeSuperQuarterlyVariances(rawSummary);
    }
    
}