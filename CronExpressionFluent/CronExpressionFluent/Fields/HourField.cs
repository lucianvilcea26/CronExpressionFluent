namespace CronExpressionFluent.Fields;

internal sealed class HourField : CronField
{
    private const string N = "Hour";

    /// <summary>
    /// Sets the hour field to the specified hour and returns the field for chaining.
    /// Valid range is 0-23.
    /// </summary>
    public HourField AtHour(int h) 
    { 
        AssertRange(N, h, 0, 23); 
        Value = h.ToString(); 
        return this; 
    }

    /// <summary>
    /// Sets the hour field to fire every hour (<c>*</c>) and returns the field for chaining.
    /// </summary>
    public HourField Every() 
    { 
        Value = "*"; 
        return this; 
    }

    /// <summary>
    /// Sets the hour field to fire every <paramref name="step"/> hours (e.g. <c>*/3</c>) and returns the field for chaining.
    /// The <paramref name="step"/> must be between 1 and 23.
    /// </summary>
    public HourField EveryHours(int step) 
    { 
        AssertStep(N, step); 
        AssertRange(N, step, 1, 23);
        Value = $"*/{step}"; 
        return this; 
    }

    /// <summary>
    /// Sets the hour field to include an inclusive range from <paramref name="start"/> to <paramref name="end"/> and returns the field for chaining.
    /// Valid range for start and end is 0-23.
    /// </summary>
    public HourField Between(int start, int end) 
    { 
        AssertRange(N, start, 0, 23); 
        AssertRange(N, end, 0, 23); 
        Value = $"{start}-{end}"; 
        return this; 
    }

    /// <summary>
    /// Sets the hour field to trigger on the specified hour values and returns the field for chaining.
    /// Pass one or more values (0-23) to select multiple hours.
    /// </summary>
    public HourField AtHours(params int[] hours)
    {
        foreach (var h in hours) AssertRange(N, h, 0, 23);
        Value = string.Join(",", hours);
        return this;
    }
}
