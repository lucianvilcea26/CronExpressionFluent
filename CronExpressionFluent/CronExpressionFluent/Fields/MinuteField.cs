namespace CronExpressionFluent.Fields;

internal sealed class MinuteField : CronField
{
    private const string N = "Minute";

    /// <summary>
    /// Sets the minute field to the specified minute and returns the field for chaining.
    /// Valid range is 0-59.
    /// </summary>
    public MinuteField AtMinute(int m) 
    { 
        AssertRange(N, m, 0, 59); 
        Value = m.ToString(); 
        return this; 
    }

    /// <summary>
    /// Sets the minute field to fire every minute (<c>*</c>) and returns the field for chaining.
    /// </summary>
    public MinuteField Every() 
    { 
        Value = "*"; 
        return this; 
    }

    /// <summary>
    /// Sets the minute field to fire every <paramref name="step"/> minutes (e.g. <c>*/5</c>) and returns the field for chaining.
    /// The <paramref name="step"/> must be between 1 and 59.
    /// </summary>
    public MinuteField EveryMinutes(int step) 
    { 
        AssertStep(N, step); 
        AssertRange(N, step, 1, 59); 
        Value = $"*/{step}"; 
        return this; 
    }

    /// <summary>
    /// Sets the minute field to include an inclusive range from <paramref name="start"/> to <paramref name="end"/> and returns the field for chaining.
    /// Valid range for start and end is 0-59.
    /// </summary>
    public MinuteField Between(int start, int end) 
    { 
        AssertRange(N, start, 0, 59); 
        AssertRange(N, end, 0, 59); 
        Value = $"{start}-{end}"; 
        return this; 
    }

    /// <summary>
    /// Sets the minute field to trigger on the specified minute values and returns the field for chaining.
    /// Pass one or more values (0-59) to select multiple minutes.
    /// </summary>
    public MinuteField AtMinutes(params int[] minutes)
    {
        foreach (var m in minutes) AssertRange(N, m, 0, 59);
        Value = string.Join(",", minutes);
        return this;
    }
}
