namespace CronExpressionFluent.Fields;

internal sealed class DayOfMonthField : CronField
{
    private const string N = "DayOfMonth";

    /// <summary>
    /// Sets the day-of-month field to the specified day and returns the field for chaining.
    /// Valid range is 1-31.
    /// </summary>
    public DayOfMonthField OnDay(int d) 
    { 
        AssertRange(N, d, 1, 31); 
        Value = d.ToString(); 
        return this; 
    }

    /// <summary>
    /// Sets the day-of-month field to trigger on the specified day numbers and returns the field for chaining.
    /// Pass one or more values (1-31) to select multiple days.
    /// </summary>
    public DayOfMonthField OnDays(params int[] days)
    {
        foreach (var d in days) AssertRange(N, d, 1, 31);
        Value = string.Join(",", days);
        return this;
    }

    /// <summary>
    /// Sets the day-of-month field to include every day (<c>*</c>) and returns the field for chaining.
    /// </summary>
    public DayOfMonthField Every() 
    { 
        Value = "*"; 
        return this; 
    }

    /// <summary>
    /// Quartz: Sets the day-of-month field to the last day of the month (<c>L</c>) and returns the field for chaining.
    /// </summary>
    public DayOfMonthField Last() 
    { 
        Value = "L"; 
        return this; 
    }

    /// <summary>
    /// Quartz: Sets the day-of-month field to the nearest weekday to the specified day (e.g., <c>15W</c>) and returns the field for chaining.
    /// Valid day range is 1-31.
    /// </summary>
    public DayOfMonthField NearestWeekday(int day) 
    { 
        AssertRange(N, day, 1, 31); 
        Value = $"{day}W"; 
        return this; 
    }

    /// <summary>
    /// Quartz: Sets the day-of-month field to the last weekday of the month (<c>LW</c>) and returns the field for chaining.
    /// </summary>
    public DayOfMonthField LastWeekday() 
    { 
        Value = "LW"; 
        return this; 
    }
}
