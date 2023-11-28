using FluentValidation;
using SuperCalculator.Api.Models.Request;

namespace SuperCalculator.Api.Validators;

public class EmployeeSuperQuarterlyVarianceRequestValidator: AbstractValidator<EmployeeSuperQuarterlyVarianceRequest>
{
    private const long MAX_FILE_SIZE = 1000000;
    public EmployeeSuperQuarterlyVarianceRequestValidator()
    {
        RuleFor(x => x.File)
            .NotNull()
            .NotEmpty()
            .WithMessage("Please provide an excel file to be processed.");

        When(x => x.File != null, () =>
        {
            RuleFor(request => request.File.Length)
                .LessThan(MAX_FILE_SIZE)
                .WithMessage("The file size exceeds the maximum file size allowed for this endpoint.");

            RuleFor(x => x.File)
                .Must(x => x.FileName.EndsWith(".xls", StringComparison.Ordinal) ||
                           x.FileName.EndsWith(".xlsx", StringComparison.Ordinal))
                .WithMessage("Invalid file extension. Allowed extension: .xls or .xlsx");
        });
    }
}