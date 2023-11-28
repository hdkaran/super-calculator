using System.Data;
using System.Text;
using ExcelDataReader;
using FluentValidation;
using SuperCalculator.Application.Models;
using SuperCalculator.Application.Models.Constants;
using SuperCalculator.Application.Validators;

namespace SuperCalculator.Application.Services;

public class RawDataReaderService: IRawDataReaderService
{
    private readonly IValidator<DataSet> _rawDataValidator;

    public RawDataReaderService(IValidator<DataSet> rawDataValidator)
    {
        _rawDataValidator = rawDataValidator;
    }
    public async Task<RawEmployeeSuperData> GetEmployeeSuperSummary(byte[] rawData)
    {
        return await Task.Run(() =>
        {
            var rawEmployeeSuperData = new RawEmployeeSuperData();
            using var reader = ExcelReaderFactory.CreateReader(new MemoryStream(rawData));

            var dataset = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            ThrowIfInvalid(dataset);
            
            foreach (DataTable table in dataset.Tables)
            {
                switch (table.TableName)
                {
                    case RawDataConstants.Disbursements:
                        rawEmployeeSuperData.Disbursements = BuildDisbursements(table);
                        break;
                    case RawDataConstants.Payslips:
                        rawEmployeeSuperData.Payslips = BuildPayslips(table);
                        break;
                    case RawDataConstants.PayCodes:
                        rawEmployeeSuperData.PayCodes = BuildPayCodes(table);
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown Datatable table name found.");
                }
            }

            return rawEmployeeSuperData;
        });
    }

    private void ThrowIfInvalid(DataSet dataset)
    {
        var validationResult = _rawDataValidator.Validate(dataset);
        if (!validationResult.IsValid)
        {
            throw new InvalidDataException(validationResult.Errors.ToString());
        }
    }

    private IEnumerable<Disbursement> BuildDisbursements(DataTable table)
    {
        var disbursements = new List<Disbursement>();

        foreach (DataRow row in table.Rows)
        {
            var disbursement = new Disbursement()
            {
                SgcAmount = Convert.ToDecimal(row[RawDataConstants.SgcAmount]),
                PaymentMade = Convert.ToDateTime(row[RawDataConstants.PaymentMade]),
                PayPeriodFrom = Convert.ToDateTime(row[RawDataConstants.PayPeriodFrom]),
                PayPeriodTo = Convert.ToDateTime(row[RawDataConstants.PayPeriodTo]),
                EmployeeCode = Convert.ToInt64(row[RawDataConstants.EmployeeCode])
            };
            
            disbursements.Add(disbursement);
        }

        return disbursements;
    }
    
    private IEnumerable<Payslip> BuildPayslips(DataTable table)
    {
        var payslips = new List<Payslip>();

        foreach (DataRow row in table.Rows)
        {
            var payslip = new Payslip()
            {
                PayslipId = new Guid(row[RawDataConstants.PayslipId].ToString() ?? throw new InvalidDataException("Provided guid is not valid.")),
                End = Convert.ToDateTime(row[RawDataConstants.End]),
                EmployeeCode = Convert.ToInt64(row[RawDataConstants.EmployeeCode]),
                Code = Convert.ToString(row[RawDataConstants.Code]),
                Amount = Convert.ToDecimal(row[RawDataConstants.Amount])
            };
            
            payslips.Add(payslip);
        }

        return payslips;
    }
    
    private IEnumerable<PayCode> BuildPayCodes(DataTable table)
    {
        var payCodes = new List<PayCode>();

        foreach (DataRow row in table.Rows)
        {
            var payCode = new PayCode()
            {
                Code = Convert.ToString(row[RawDataConstants.PayCode]),
                Treatment = PayCode.GetOteFromString(Convert.ToString(row[RawDataConstants.OteTreatment]) ?? throw new InvalidDataException("Field Ote_treatment is not valid"))
            };
            
            payCodes.Add(payCode);
        }

        return payCodes;
    }
}