using SuperCalculator.Application.Models;
using SuperCalculator.Application.Models.Constants;
using SuperCalculator.Application.Services;

namespace SuperCalculator.Application.Builders;

public class EmployeeSuperQuarterlyVarianceSummaryBuilder: IEmployeeSuperQuarterlyVarianceSummaryBuilder
{
    private readonly IDateTimeService _dateTimeService;

    public EmployeeSuperQuarterlyVarianceSummaryBuilder(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }
    public EmployeeSuperQuarterlyVarianceSummary CreateEmployeeSuperQuarterlyVariances(RawEmployeeSuperData rawSummary)
    {
        var summary = new EmployeeSuperQuarterlyVarianceSummary();
        var employees = rawSummary.Disbursements.Select(x => x.EmployeeCode).Distinct();

        foreach (var emp in employees)
        {
            summary.EmployeeSuperQuarterlyVariances ??= new List<EmployeeSuperQuarterlyVariance>();
            summary.EmployeeSuperQuarterlyVariances.Add(GetEmployeeSummary(rawSummary, emp));
        }

        return summary;
    }
    
    private EmployeeSuperQuarterlyVariance GetEmployeeSummary(RawEmployeeSuperData rawSummary, long emp)
    {
        var quarterlyVariance = new EmployeeSuperQuarterlyVariance();
        quarterlyVariance.EmployeeCode = emp;
        
        var oteCodes = rawSummary.PayCodes.Where(x => x.Treatment == OteTreatment.OTE).Select(x => x.Code);
        var employeePayslips = rawSummary.Payslips.Where(x => x.EmployeeCode == emp).ToList();
        var employeeDisbursements = rawSummary.Disbursements.Where(x => x.EmployeeCode == emp).ToList();
        var employeePayDates = employeePayslips.Select(x => x.End).ToList();
        var quartersEmployeeWasPaidIn = _dateTimeService.GetQuartersForDates(employeePayDates).DistinctBy(x=>x.From);

        foreach (var quarter in quartersEmployeeWasPaidIn.OrderBy(x=>x.From))
        {
            quarterlyVariance.QuarterlyEmployeeSuperVariances ??= new List<EmployeeSuperVariance>();
            
            var employeeSuperVariance = new EmployeeSuperVariance();
            employeeSuperVariance.FromDate = quarter.From;
            employeeSuperVariance.ToDate = quarter.To;
            employeeSuperVariance.TotalOte = Math.Round(employeePayslips
                .Where(x => x.End >= quarter.From && x.End <= quarter.To && oteCodes.Contains(x.Code))
                .Select(x => x.Amount)
                .Sum(), 2);
            employeeSuperVariance.TotalSuperPayable = Math.Round(employeeSuperVariance.TotalOte * SuperConstants.RateOfSuper, 2);
            employeeSuperVariance.TotalDisbursed = Math.Round(employeeDisbursements
                .Where(x =>
                    x.PaymentMade >= new DateTime(quarter.From.Year, quarter.From.Month, DateTimeConstants.SuperPayDayStart) &&
                    x.PaymentMade <= new DateTime(quarter.To.Year, quarter.To.Month, DateTimeConstants.SuperPayDayEnd).AddMonths(1) &&
                    x.EmployeeCode == emp)
                .Select(x => x.SgcAmount)
                .Sum(), 2);
            employeeSuperVariance.Variance =
                Math.Round(employeeSuperVariance.TotalSuperPayable - employeeSuperVariance.TotalDisbursed, 2);

            quarterlyVariance.QuarterlyEmployeeSuperVariances.Add(employeeSuperVariance);
        }
        return quarterlyVariance;
    }
}