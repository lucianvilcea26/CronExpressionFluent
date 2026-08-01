namespace CronExpressionFluent.Fields;

internal sealed class MonthField : CronField
{
    private const string N = "Month";
    private static readonly string[] Abbr = { "", "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

    /// <summary>
    /// Sets the month field to the specified <see cref="Enums.Month"/> value and returns the field for chaining.
    /// The month will be converted to its three-letter abbreviation (e.g., JAN).
    /// </summary>
    public MonthField InMonth(Enums.Month month) 
    { 
        Value = Abbr[(int)month]; 
        return this; 
    }

    /// <summary>
    /// Sets the month field to include every month (<c>*</c>) and returns the field for chaining.
    /// </summary>
    public MonthField Every() 
    { 
        Value = "*"; 
        return this; 
    }

    /// <summary>
    /// Sets the month field to trigger during any of the specified <see cref="Enums.Month"/> values and returns the field for chaining.
    /// Multiple months are converted to their three-letter abbreviations and joined by commas.
    /// </summary>
    public MonthField InMonths(params Enums.Month[] months)
    {
        Value = string.Join(",", months.Select(m => Abbr[(int)m]));
        return this;
    }

    /// <summary>
    /// Sets the month field to include an inclusive range between the specified months and returns the field for chaining.
    /// </summary>
    public MonthField BetweenMonths(Enums.Month start, Enums.Month end)
    {
        Value = $"{Abbr[(int)start]}-{Abbr[(int)end]}";
        return this;
    }

    /// <summary>
    /// Sets the month field to the specified numeric month value (1-12) and returns the field for chaining.
    /// Use this when numeric month values are more convenient than the enum.
    /// </summary>
    public MonthField InMonthNumber(int month) 
    { 
        AssertRange(N, month, 1, 12); 
        Value = month.ToString(); 
        return this; 
    }

    /// <summary>
    /// Sets the month field to trigger during any of the specified numeric month values and returns the field for chaining.
    /// Pass one or more values in the range 1-12.
    /// </summary>
    public MonthField InMonthNumbers(params int[] months)
    {
        foreach (var m in months) AssertRange(N, m, 1, 12);
        Value = string.Join(",", months);
        return this;
    }
}
