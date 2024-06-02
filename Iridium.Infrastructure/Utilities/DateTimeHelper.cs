namespace Iridium.Infrastructure.Utilities;

public class DateTimeHelper
{
    public static void SwapDatesIfNeeded(ref DateTime startDate, ref DateTime? endDate)
    {
        if (endDate.HasValue && startDate > endDate)
        {
            var tempDate = endDate.Value;
            endDate = startDate;
            startDate = tempDate;
        }
    }

    public static void SwapDatesIfNeeded(ref DateTime startDate, ref DateTime endDate)
    {
        if (startDate > endDate)
            (endDate, startDate) = (startDate, endDate);
    }

    public static IEnumerable<DateTime> AllDatesBetween(DateTime startDate, DateTime endDate)
    {
        for (var day = startDate.Date; day <= endDate; day = day.AddDays(1))
            yield return day;
    }

    public static bool OverlappingPeriods(DateTime Period1Start, DateTime Period1End, DateTime Period2Start,
        DateTime Period2End)
    {
        return Period1Start < Period2End && Period2Start < Period1End;
    }
}

public static class TimeSpanHelpers
{
    private static string FormatPart(int quantity, string name)
    {
        return quantity > 0 ? $"{quantity} {name}" : null;
    }

    public static string ToPrettyFormat(this TimeSpan timeSpan)
    {
        return string.Join(", ",
            new[]
            {
                FormatPart(timeSpan.Days, "gün"), FormatPart(timeSpan.Hours, "saat"),
                FormatPart(timeSpan.Minutes, "dakika")
            }.Where(x => x != null));
    }
}

public class Period
{
    public Period(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public bool IsOverlapping(Period period)
    {
        return DateTimeOverlapHelper.IsOverlapping(this, period);
    }
}

public class DateTimeOverlapHelper
{
    public static bool IsOverlapping(Period first, Period second)
    {
        return Math.Max(first.StartDate.Ticks, second.StartDate.Ticks) <=
               Math.Min(first.EndDate.Ticks, second.EndDate.Ticks);
    }
}