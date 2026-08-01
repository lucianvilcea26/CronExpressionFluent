using CronExpressionFluent.Enums;

namespace CronExpressionFluent;

/// <summary>
/// Entry point and convenience API for building cron expressions fluently.
/// Provides factory methods to create CronBuilder instances and common preset expressions.
/// </summary>
/// <example>
/// <code>
/// string cron = CronExpression.Create()
///     .EveryMinutes(5)
///     .Build();
/// // → "*/5 * * * *"
/// </code>
/// </example>
public static class CronExpression
{
    /// <summary>
    /// Creates a new CronBuilder configured for Unix (5-field) cron format.
    /// Use this when you need to build standard Unix-style cron expressions.
    /// </summary>
    public static CronBuilder Create() => new(CronFormat.Unix);

    /// <summary>
    /// Creates a new CronBuilder configured for the specified <see cref="CronFormat"/>.
    /// Pass <see cref="CronFormat.Quartz"/> to enable Quartz (6-field) extensions.
    /// </summary>
    public static CronBuilder Create(CronFormat format) => new(format);

    /// <summary>
    /// Returns a cron expression that triggers every minute (<c>* * * * *</c>).
    /// Use this convenience method when you need a simple per-minute schedule.
    /// </summary>
    public static string EveryMinute() => "* * * * *";

    /// <summary>
    /// Returns a cron expression that triggers every <paramref name="n"/> minutes.
    /// This is equivalent to Create().EveryMinutes(n).Build().
    /// </summary>
    public static string EveryNMinutes(int n) => Create().EveryMinutes(n).Build();

    /// <summary>
    /// Returns a cron expression that triggers once per hour at minute 0 (top of the hour).
    /// Equivalent to Create().AtMinute(0).Build().
    /// </summary>
    public static string Hourly() => Create().AtMinute(0).Build();

    /// <summary>
    /// Returns a cron expression that triggers once per hour at the specified <paramref name="minute"/>.
    /// Equivalent to Create().AtMinute(minute).Build().
    /// </summary>
    public static string HourlyAt(int minute) => Create().AtMinute(minute).Build();

    /// <summary>
    /// Returns a cron expression that triggers daily at midnight (<c>0 0 * * *</c>).
    /// Equivalent to Create().AtMinute(0).AtHour(0).Build().
    /// </summary>
    public static string Daily() => Create().AtMinute(0).AtHour(0).Build();

    /// <summary>
    /// Returns a cron expression that triggers daily at the specified <paramref name="hour"/> and optional <paramref name="minute"/>.
    /// Equivalent to Create().AtMinute(minute).AtHour(hour).Build().
    /// </summary>
    public static string DailyAt(int hour, int minute = 0) =>
        Create().AtMinute(minute).AtHour(hour).Build();

    /// <summary>
    /// Returns a cron expression that triggers weekly on the specified <paramref name="day"/> at midnight.
    /// Equivalent to Create().AtMinute(0).AtHour(0).OnWeekday(day).Build().
    /// </summary>
    public static string Weekly(CronDayOfWeek day) =>
        Create().AtMinute(0).AtHour(0).OnWeekday(day).Build();

    /// <summary>
    /// Returns a cron expression that triggers monthly on the 1st day at midnight.
    /// Equivalent to Create().AtMinute(0).AtHour(0).OnDay(1).Build().
    /// </summary>
    public static string Monthly() =>
        Create().AtMinute(0).AtHour(0).OnDay(1).Build();

    /// <summary>
    /// Returns a cron expression that triggers yearly on January 1st at midnight.
    /// Equivalent to Create().AtMinute(0).AtHour(0).OnDay(1).InMonth(Month.January).Build().
    /// </summary>
    public static string Yearly() =>
        Create().AtMinute(0).AtHour(0).OnDay(1).InMonth(Month.January).Build();
}
