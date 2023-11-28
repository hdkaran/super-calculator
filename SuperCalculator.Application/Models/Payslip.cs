namespace SuperCalculator.Application.Models;

public class Payslip
{
    public Guid PayslipId { get; set; }
    public DateTime End { get; set; }
    public long EmployeeCode { get; set; }
    public string Code { get; set; }
    public decimal Amount { get; set; }
}