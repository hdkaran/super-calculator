using SuperCalculator.Application.Models;

namespace SuperCalculator.Application.Tests.DataGenerator;

public class TestDataGenerator
{
    public static RawEmployeeSuperData CreateSampleRawEmployeeSuperData()
    {
        var disbursements = GenerateDisbursements();
        var payslips = GeneratePayslips();
        var payCodes = GeneratePayCodes();

        return new RawEmployeeSuperData
        {
            Disbursements = disbursements,
            Payslips = payslips,
            PayCodes = payCodes
        };
    }

    private static IEnumerable<Disbursement> GenerateDisbursements()
    {
        var disbursements = new List<Disbursement>
        {
            new Disbursement
            {
                SgcAmount = 1500.50m,
                PaymentMade = new DateTime(2023, 1, 15),
                PayPeriodFrom = new DateTime(2023, 1, 1),
                PayPeriodTo = new DateTime(2023, 1, 31),
                EmployeeCode = 123456
            },
        };

        return disbursements;
    }

    private static IEnumerable<Payslip> GeneratePayslips()
    {
        var payslips = new List<Payslip>
        {
            new Payslip
            {
                PayslipId = Guid.NewGuid(),
                End = new DateTime(2023, 1, 31),
                EmployeeCode = 123456,
                Code = "SAL",
                Amount = 5000.75m
            },
        };

        return payslips;
    }

    private static IEnumerable<PayCode> GeneratePayCodes()
    {
        var payCodes = new List<PayCode>
        {
            new PayCode { Code = "SAL", Treatment = OteTreatment.NotOte },
            new PayCode { Code = "BON", Treatment = OteTreatment.NotOte },
            new PayCode { Code = "OTE1", Treatment = OteTreatment.OTE },
        };

        return payCodes;
    }
}