namespace CronExpressionFluent.Fields;

internal sealed class SecondField : CronField
{
    private const string N = "Second";

    public SecondField() { Value = "0"; }

    /// <summary>
    /// Sets the seconds field to the specified second and returns the field for chaining.
    /// Valid range is 0-59.
    /// </summary>
    public SecondField AtSecond(int s) 
    { 
        AssertRange(N, s, 0, 59); 
        Value = s.ToString(); 
        return this; 
    }

    /// <summary>
    /// Sets the seconds field to fire every second (<c>*</c>) and returns the field for chaining.
    /// </summary>
    public SecondField Every() 
    { 
        Value = "*"; 
        return this; 
    }

    /// <summary>
    /// Sets the seconds field to fire every <paramref name="step"/> seconds (e.g. <c>*/5</c>) and returns the field for chaining.
    /// The <paramref name="step"/> must be between 1 and 59.
    /// </summary>
    public SecondField EverySeconds(int step) 
    { 
        AssertStep(N, step); 
        AssertRange(N, step, 1, 59); 
        Value = $"*/{step}"; 
        return this; 
    }

    /// <summary>
    /// Sets the seconds field to include an inclusive range from <paramref name="start"/> to <paramref name="end"/> and returns the field for chaining.
    /// Valid range for start and end is 0-59.
    /// </summary>
    public SecondField Between(int start, int end) 
    { 
        AssertRange(N, start, 0, 59); 
        AssertRange(N, end, 0, 59); 
        Value = $"{start}-{end}"; 
        return this; 
    }
}
