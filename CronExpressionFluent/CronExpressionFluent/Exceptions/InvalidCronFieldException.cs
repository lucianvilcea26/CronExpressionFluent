namespace CronExpressionFluent.Exceptions;

/// <summary>
/// Thrown when a cron field value violates constraints.
/// </summary>
public class InvalidCronFieldException : ArgumentException
{
    public string FieldName { get; }

    public InvalidCronFieldException(string fieldName, string message)
        : base($"Invalid value for cron field '{fieldName}': {message}")
    {
        FieldName = fieldName;
    }
}
