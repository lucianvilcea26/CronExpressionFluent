namespace CronExpressionFluent.Enums;

/// <summary>
/// Specifies the cron expression format.
/// </summary>
/// <summary>
/// Specifies the cron expression format.
/// </summary>
public enum CronFormat
{
    /// <summary>
    /// Unix-style 5-field format: minute hour day-of-month month day-of-week.
    /// Use this for standard cron expressions without seconds or Quartz extensions.
    /// </summary>
    Unix,

    /// <summary>
    /// Quartz-style 6-field format: second minute hour day-of-month month day-of-week.
    /// Use this to enable Quartz-specific features such as seconds and special day-of-month/week markers.
    /// </summary>
    Quartz
}
