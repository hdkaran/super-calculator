using System.Data;
using FluentValidation;
using SuperCalculator.Application.Models.Constants;

namespace SuperCalculator.Application.Validators;

public class RawEmployeeSuperDataSetValidator: AbstractValidator<DataSet>
{
    private readonly List<string> _disbursementsColumns = new()
    {
        RawDataConstants.SgcAmount,
        RawDataConstants.PaymentMade,
        RawDataConstants.PayPeriodFrom,
        RawDataConstants.PayPeriodTo,
        RawDataConstants.EmployeeCode
    };
    private readonly List<string> _payslipsColumns = new()
    {
        RawDataConstants.PayslipId,
        RawDataConstants.End,
        RawDataConstants.EmployeeCode,
        RawDataConstants.Code,
        RawDataConstants.Amount
    };

    private readonly List<string> _payCodesColumns = new()
    {
        RawDataConstants.PayCode,
        RawDataConstants.OteTreatment
    };
    
    public RawEmployeeSuperDataSetValidator()
    {
        RuleFor(x => x.Tables.Count).GreaterThanOrEqualTo(3).WithMessage("Three worksheets are required with names: Disbursements, Payslips and PayCodes.");

        When(x => x.Tables.Count >= 3, () =>
        {
            RuleFor(set => set.Tables).Must(DoesAllThreeSetsExist)
                .WithMessage("Worksheets must be named 'Disbursements', 'Payslips' and 'PayCodes'.");
            RuleFor(set => set.Tables).Must(HaveValidTableStructure).WithMessage("Some tables are missing required columns.");
        });
    }

    private bool DoesAllThreeSetsExist(DataTableCollection tables)
    {
        return tables.Contains(RawDataConstants.Disbursements) && tables.Contains(RawDataConstants.Payslips) &&
               tables.Contains(RawDataConstants.PayCodes);
    }
    private bool HaveValidTableStructure(DataTableCollection tables)
    {
        foreach (DataTable table in tables)
        {
            switch (table.TableName)
            {
                case RawDataConstants.Disbursements:
                    return ValidateTableStructure(table, _disbursementsColumns);

                case RawDataConstants.Payslips:
                    return ValidateTableStructure(table, _payslipsColumns);

                case RawDataConstants.PayCodes:
                    return ValidateTableStructure(table, _payCodesColumns);

                default:
                    return false;
            }
        }
        return true;
    }
    private bool ValidateTableStructure(DataTable table, List<string> expectedColumnNames)
    {
        return table.Columns.Count == expectedColumnNames.Count &&
               ValidateColumnNames(table, expectedColumnNames);
    }
    private bool ValidateColumnNames(DataTable table, List<string> expectedColumnNames)
    {
        foreach (var columnName in expectedColumnNames)
        {
            if (!table.Columns.Contains(columnName) || table.Columns[columnName]!.ColumnName != columnName)
            {
                return false;
            }
        }
        return true;
    }
}