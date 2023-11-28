using SuperCalculator.Application.Models;

namespace SuperCalculator.Application.Services;

public interface IDateTimeService
{
    List<Quarter> GetQuartersForDates(List<DateTime> dates);
}