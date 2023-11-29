using System.Data;
using FluentValidation.TestHelper;
using SuperCalculator.Application.Models.Constants;
using SuperCalculator.Application.Validators;
using Xunit;

namespace SuperCalculator.Application.Tests.Validators;

public class RawEmployeeSuperDataSetValidatorTests
{
    [Fact]
    public void ShouldHaveErrorWhenLessThanThreeTables()
    {
        // Arrange
        var dataSet = new DataSet();

        // Act
        var validator = new RawEmployeeSuperDataSetValidator();
        var result = validator.TestValidate(dataSet);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Tables.Count);
    }
    [Fact]
    public void ShouldHaveErrorWhenTableAreMissingInDataset()
    {
        // Arrange
        var dataSet = new DataSet();
        dataSet.Tables.Add(new DataTable("InvalidTable"));

        // Act
        var validator = new RawEmployeeSuperDataSetValidator();
        var result = validator.TestValidate(dataSet);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Tables.Count)
            .WithErrorMessage("Three worksheets are required with names: Disbursements, Payslips and PayCodes.");
    }
    [Fact]
    public void ShouldHaveErrorWhenMissingRequiredColumns()
    {
        // Arrange
        var dataSet = new DataSet();
        dataSet.Tables.Add(new DataTable(RawDataConstants.Disbursements));
        dataSet.Tables.Add(new DataTable(RawDataConstants.Payslips));
        dataSet.Tables.Add(new DataTable(RawDataConstants.PayCodes));


        // Act
        var validator = new RawEmployeeSuperDataSetValidator();
        var result = validator.TestValidate(dataSet);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Tables)
            .WithErrorMessage("Some tables are missing required columns. ##Could be a nicer error message##");
    }
}