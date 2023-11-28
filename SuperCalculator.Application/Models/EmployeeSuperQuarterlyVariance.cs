namespace SuperCalculator.Application.Models;
public class EmployeeSuperQuarterlyVariance
{
    public long EmployeeCode { get; set; }
    public List<EmployeeSuperVariance> QuarterlyEmployeeSuperVariances { get; set; }
}