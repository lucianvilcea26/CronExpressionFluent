using CronExpressionFluent.Exceptions;

namespace CronExpressionFluent.Fields;

internal sealed class DayOfWeekField : CronField
{
    private const string N = "DayOfWeek";
    private static readonly string[] Abbr = { "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT" };

    private static string DayStr(Enums.CronDayOfWeek d) => Abbr[(int)d];

    /// <summary>
    /// Sets the day-of-week field to the specified <see cref="Enums.CronDayOfWeek"/> value and returns the field for chaining.
    /// </summary>
    public DayOfWeekField OnDay(Enums.CronDayOfWeek day) 
    { 
        Value = DayStr(day); 
        return this; 
    }

    /// <summary>
    /// Sets the day-of-week field to include every day (<c>*</c>) and returns the field for chaining.
    /// </summary>
    public DayOfWeekField Every() 
    { 
        Value = "*"; 
        return this; 
    }

    /// <summary>
    /// Sets the day-of-week field to trigger on any of the specified <see cref="Enums.CronDayOfWeek"/> values and returns the field for chaining.
    /// Multiple values are joined by commas.
    /// </summary>
    public DayOfWeekField OnDays(params Enums.CronDayOfWeek[] days)
    {
        Value = string.Join(",", days.Select(DayStr));
        return this;
    }

    /// <summary>
    /// Sets the day-of-week field to include an inclusive range from <paramref name="start"/> to <paramref name="end"/> and returns the field for chaining.
    /// </summary>
    public DayOfWeekField Between(Enums.CronDayOfWeek start, Enums.CronDayOfWeek end)
    {
        Value = $"{DayStr(start)}-{DayStr(end)}";
        return this;
    }

    /// <summary>
    /// Sets the day-of-week field to weekdays (<c>MON-FRI</c>) and returns the field for chaining.
    /// </summary>
    public DayOfWeekField Weekdays() 
    { 
        Value = "MON-FRI"; 
        return this; 
    }

    /// <summary>
    /// Sets the day-of-week field to weekend days (<c>SAT,SUN</c>) and returns the field for chaining.
    /// </summary>
    public DayOfWeekField Weekends() 
    { 
        Value = "SAT,SUN"; 
        return this; 
    }

    /// <summary>
    /// Quartz: Sets the day-of-week field to the nth occurrence of <paramref name="day"/> in the month (e.g., <c>MON#2</c>) and returns the field for chaining.
    /// The <paramref name="nth"/> value must be between 1 and 5.
    /// </summary>
    public DayOfWeekField NthOfMonth(Enums.CronDayOfWeek day, int nth)
    {
        if (nth < 1 || nth > 5)
            throw new InvalidCronFieldException(N, $"Nth value must be 1-5, got {nth}.");
        Value = $"{DayStr(day)}#{nth}";
        return this;
    }

    /// <summary>
    /// Quartz: Sets the day-of-week field to the last occurrence of the specified weekday in the month (e.g., <c>FRIL</c>) and returns the field for chaining.
    /// </summary>
    public DayOfWeekField LastOfMonth(Enums.CronDayOfWeek day) 
    { 
        Value = $"{DayStr(day)}L"; 
        return this; 
    }

    /// <summary>
    /// Sets the day-of-week field by numeric value (0 = Sunday, 6 = Saturday) and returns the field for chaining.
    /// Useful for Unix compatibility where days are represented by numbers.
    /// </summary>
    public DayOfWeekField OnDayNumber(int day) 
    { 
        AssertRange(N, day, 0, 6); 
        Value = day.ToString(); 
        return this; 
    }

    /// <summary>
    /// Sets the day-of-week field to include numeric day-of-week values within the inclusive range from <paramref name="start"/> to <paramref name="end"/> and returns the field for chaining.
    /// Valid range for numeric values is 0-6.
    /// </summary>
    public DayOfWeekField BetweenNumbers(int start, int end)
    {
        AssertRange(N, start, 0, 6); AssertRange(N, end, 0, 6);
        Value = $"{start}-{end}";
        return this;
    }
}
