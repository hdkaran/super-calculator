using SuperCalculator.Application.Models;

namespace SuperCalculator.Application.Services;

public interface IEmployeeSuperService
{
    Task<EmployeeSuperQuarterlyVarianceSummary> ProcessEmployeeSuperQuarterlyVariances(byte[] rawData);
}