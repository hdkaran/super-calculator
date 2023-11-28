using SuperCalculator.Application.Models;

namespace SuperCalculator.Application.Services;

public class DateTimeService : IDateTimeService
{
    public List<Quarter> GetQuartersForDates(List<DateTime> dates)
    {
        if (dates == null || !dates.Any())
        {
            throw new InvalidOperationException("Cannot get quarters for an empty or null date list");
        }
        var quarters = new List<Quarter>();

        foreach (var date in dates)
        {
            var quarterRange = GetQuarterRange(date);
            quarters.Add(quarterRange);
        }

        return quarters;
    }
    
    private static Quarter GetQuarterRange(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;

        DateTime from, to;

        if (month >= 1 && month <= 3)
        {
            from = new DateTime(year, 1, 1);
            to = new DateTime(year, 3, 31);
        }
        else if (month >= 4 && month <= 6)
        {
            from = new DateTime(year, 4, 1);
            to = new DateTime(year, 6, 30);
        }
        else if (month >= 7 && month <= 9)
        {
            from = new DateTime(year, 7, 1);
            to = new DateTime(year, 9, 30);
        }
        else
        {
            from = new DateTime(year, 10, 1);
            to = new DateTime(year, 12, 31);
        }

        return new Quarter { From = from, To = to };
    }
}