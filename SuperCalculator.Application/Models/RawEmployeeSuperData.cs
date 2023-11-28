namespace SuperCalculator.Application.Models;

public class RawEmployeeSuperData
{
    public IEnumerable<Disbursement> Disbursements { get; set; }
    public IEnumerable<Payslip> Payslips { get; set; }
    public IEnumerable<PayCode> PayCodes { get; set; }
}