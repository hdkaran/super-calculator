using SuperCalculator.Application.Models;

namespace SuperCalculator.Application.Builders;

public interface IEmployeeSuperQuarterlyVarianceSummaryBuilder
{
    EmployeeSuperQuarterlyVarianceSummary CreateEmployeeSuperQuarterlyVariances(RawEmployeeSuperData rawData);
}