using CronExpressionFluent.Exceptions;

namespace CronExpressionFluent.Fields;

internal abstract class CronField
{
    public string Value { get; protected set; } = "*";

    protected static void AssertRange(string field, int value, int min, int max)
    {
        if (value < min || value > max)
            throw new InvalidCronFieldException(field,
                $"Value {value} is out of range [{min}, {max}].");
    }

    protected static void AssertStep(string field, int step)
    {
        if (step <= 0)
            throw new InvalidCronFieldException(field,
                $"Step value must be positive, got {step}.");
    }

    /// <summary>
    /// Marks this field as unspecified by setting its value to the Quartz-specific marker <c>"?"</c>.
    /// Use this when constructing Quartz (6-field) cron expressions to indicate the field is intentionally unset
    /// (for example when day-of-month and day-of-week are mutually exclusive).
    /// Note: this does not return a builder and is intended for internal resolution logic.
    /// </summary>
    public void Unspecified() => Value = "?";

    public override string ToString() => Value;
}