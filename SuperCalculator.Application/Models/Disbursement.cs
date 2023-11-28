namespace SuperCalculator.Application.Models;

public class Disbursement
{
    public decimal SgcAmount { get; set; }
    public DateTime PaymentMade { get; set; }
    public DateTime PayPeriodFrom { get; set; }
    public DateTime PayPeriodTo { get; set; }
    public long EmployeeCode { get; set; }
}