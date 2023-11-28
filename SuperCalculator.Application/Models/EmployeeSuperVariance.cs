namespace SuperCalculator.Application.Models;

public class EmployeeSuperVariance
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal TotalOte { get; set; }
    public decimal TotalSuperPayable { get; set; }
    public decimal TotalDisbursed { get; set; }
    public decimal Variance { get; set; }
}