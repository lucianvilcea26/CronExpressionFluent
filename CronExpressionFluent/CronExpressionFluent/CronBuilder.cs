using CronExpressionFluent.Enums;
using CronExpressionFluent.Fields;

namespace CronExpressionFluent;

/// <summary>
/// Fluent builder for cron expressions. Supports Unix (5-part) and Quartz (6-part) formats.
/// </summary>
public sealed class CronBuilder
{
    private readonly CronFormat _format;

    private readonly SecondField _second = new();
    private readonly MinuteField _minute = new();
    private readonly HourField _hour = new();
    private readonly DayOfMonthField _dayOfMonth = new();
    private readonly MonthField _month = new();
    private readonly DayOfWeekField _dayOfWeek = new();

    private bool _dayOfWeekSet;
    private bool _dayOfMonthSet;

    internal CronBuilder(CronFormat format) => _format = format;

    /// <summary>
    /// Configures the second field to the specified second (Quartz only) and returns the builder for chaining.
    /// Use this to target a single second value within the minute when building the expression.
    /// </summary>
    public CronBuilder AtSecond(int second) 
    { 
        EnsureQuartz(nameof(AtSecond)); 
        _second.AtSecond(second); 
        return this; 
    }

    /// <summary>
    /// Configures the second field to fire every second (Quartz only) and returns the builder for chaining.
    /// Use this to include all seconds when composing the cron expression.
    /// </summary>
    public CronBuilder EverySecond() 
    { 
        EnsureQuartz(nameof(EverySecond)); 
        _second.Every(); return this; 
    }

    /// <summary>
    /// Configures the second field to fire every <paramref name="step"/> seconds (Quartz only) and returns the builder for chaining.
    /// Use this to set a seconds interval when composing the cron expression.
    /// </summary>
    public CronBuilder EverySeconds(int step) 
    { 
        EnsureQuartz(nameof(EverySeconds)); 
        _second.EverySeconds(step); 
        return this; 
    }

    /// <summary>
    /// Configures the second field to include seconds within the inclusive range from <paramref name="start"/> to <paramref name="end"/> (Quartz only) and returns the builder for chaining.
    /// </summary>
    public CronBuilder BetweenSeconds(int start, int end) 
    { 
        EnsureQuartz(nameof(BetweenSeconds)); 
        _second.Between(start, end); 
        return this; 
    }

    /// <summary>
    /// Configures the minute field to the specified minute and returns the builder for chaining.
    /// Use this to target a single minute within the hour when composing the expression.
    /// </summary>
    public CronBuilder AtMinute(int minute) 
    { 
        _minute.AtMinute(minute); 
        return this; 
    }

    /// <summary>
    /// Configures the minute field to fire every minute and returns the builder for chaining.
    /// Use this to include all minutes when composing the cron expression.
    /// </summary>
    public CronBuilder EveryMinute() 
    { 
        _minute.Every(); 
        return this; 
    }

    /// <summary>
    /// Configures the minute field to fire every <paramref name="step"/> minutes and returns the builder for chaining.
    /// Use this to set a minute interval when composing the cron expression.
    /// </summary>
    public CronBuilder EveryMinutes(int step) 
    { 
        _minute.EveryMinutes(step); 
        return this; 
    }

    /// <summary>
    /// Configures the minute field to include minutes within the inclusive range from <paramref name="start"/> to <paramref name="end"/> and returns the builder for chaining.
    /// </summary>
    public CronBuilder BetweenMinutes(int start, int end) 
    { 
        _minute.Between(start, end); 
        return this; 
    }

    /// <summary>
    /// Configures the minute field to trigger on the specified minute values and returns the builder for chaining.
    /// Pass one or more minute values to trigger on multiple minutes each hour when composing the expression.
    /// </summary>
    public CronBuilder AtMinutes(params int[] minutes) 
    { 
        _minute.AtMinutes(minutes); 
        return this; 
    }

    /// <summary>
    /// Configures the hour field to the specified hour and returns the builder for chaining.
    /// Use this to target a single hour within the day when composing the expression.
    /// </summary>
    public CronBuilder AtHour(int hour) 
    { 
        _hour.AtHour(hour); 
        return this; 
    }

    /// <summary>
    /// Configures the hour field to fire every hour and returns the builder for chaining.
    /// Use this to include all hours when composing the cron expression.
    /// </summary>
    public CronBuilder EveryHour() 
    { 
        _hour.Every(); 
        return this; 
    }

    /// <summary>
    /// Configures the hour field to fire every <paramref name="step"/> hours and returns the builder for chaining.
    /// Use this to set an hourly interval when composing the cron expression.
    /// </summary>
    public CronBuilder EveryHours(int step) 
    { 
        _hour.EveryHours(step); 
        return this; 
    }

    /// <summary>
    /// Configures the hour field to include hours within the inclusive range from <paramref name="start"/> to <paramref name="end"/> and returns the builder for chaining.
    /// </summary>
    public CronBuilder BetweenHours(int start, int end) 
    { 
        _hour.Between(start, end); 
        return this; 
    }

    /// <summary>
    /// Configures the hour field to trigger on the specified hour values and returns the builder for chaining.
    /// Pass one or more hour values to trigger on multiple hours each day when composing the expression.
    /// </summary>
    public CronBuilder AtHours(params int[] hours) 
    { 
        _hour.AtHours(hours); 
        return this; 
    }

    /// <summary>
    /// Configures the day-of-month field to the specified day and returns the builder for chaining.
    /// Use this to target a single day number within each month when composing the expression.
    /// </summary>
    public CronBuilder OnDay(int day) 
    { 
        _dayOfMonth.OnDay(day); 
        _dayOfMonthSet = true; 
        return this; 
    }

    /// <summary>
    /// Configures the day-of-month field to trigger on the specified day numbers and returns the builder for chaining.
    /// Pass multiple values to trigger on several days each month when composing the expression.
    /// </summary>
    public CronBuilder OnDays(params int[] days) 
    { 
        _dayOfMonth.OnDays(days);
        _dayOfMonthSet = true; 
        return this;
    }
    
    /// <summary>
    /// Configures the day-of-month field to include every day and returns the builder for chaining.
    /// Equivalent to selecting all possible day-of-month values when composing the expression.
    /// </summary>
    public CronBuilder EveryDay() 
    { 
        _dayOfMonth.Every(); 
        return this; 
    }

    /// <summary>
    /// Creates a preset to generate a schedule triggering on the last day of the month (Quartz only).
    /// Use this when you want the task to run on the final calendar day for each month.
    /// </summary>
    public CronBuilder OnLastDayOfMonth() 
    { 
        EnsureQuartz(nameof(OnLastDayOfMonth)); 
        _dayOfMonth.Last(); 
        _dayOfMonthSet = true; 
        return this; 
    }

    /// <summary>
    /// Creates a preset to generate a schedule triggering on the weekday nearest to the specified day-of-month (Quartz only).
    /// For example, if the requested day falls on a weekend, the schedule will use the closest weekday instead.
    /// </summary>
    public CronBuilder OnWeekdayNearestTo(int day) 
    { 
        EnsureQuartz(nameof(OnWeekdayNearestTo)); 
        _dayOfMonth.NearestWeekday(day); 
        _dayOfMonthSet = true; 
        return this; 
    }

    /// <summary>
    /// Creates a preset to generate a schedule triggering on the last weekday of the month (Quartz only).
    /// Use this to run actions on the final business day of each month.
    /// </summary>
    public CronBuilder OnLastWeekdayOfMonth() 
    { 
        EnsureQuartz(nameof(OnLastWeekdayOfMonth)); 
        _dayOfMonth.LastWeekday(); 
        _dayOfMonthSet = true; 
        return this; 
    }

    /// <summary>
    /// Configures the month field to the specified <see cref="Month"/> and returns the builder for chaining.
    /// Use the <see cref="Month"/> enum to select the month by name when composing the expression.
    /// </summary>
    public CronBuilder InMonth(Month month) 
    { 
        _month.InMonth(month); 
        return this; 
    }

    /// <summary>
    /// Configures the month field to include every month and returns the builder for chaining.
    /// Equivalent to selecting all months of the year when composing the expression.
    /// </summary>
    public CronBuilder EveryMonth() 
    { 
        _month.Every(); 
        return this; 
    }

    /// <summary>
    /// Configures the month field to trigger during any of the specified <see cref="Month"/> values and returns the builder for chaining.
    /// Pass one or more values to select multiple months when composing the expression.
    /// </summary>
    public CronBuilder InMonths(params Month[] months) 
    { 
        _month.InMonths(months); 
        return this; 
    }

    /// <summary>
    /// Configures the month field to include months within the inclusive range from <paramref name="start"/> to <paramref name="end"/> and returns the builder for chaining.
    /// </summary>
    public CronBuilder BetweenMonths(Month start, Month end) 
    { 
        _month.BetweenMonths(start, end); 
        return this; 
    }

    /// <summary>
    /// Configures the month field to the specified month number (1-12) and returns the builder for chaining.
    /// Use numeric values when the enum is not convenient when composing the expression.
    /// </summary>
    public CronBuilder InMonthNumber(int month) 
    { 
        _month.InMonthNumber(month); 
        return this; 
    }

    /// <summary>
    /// Configures the month field to trigger during any of the specified month numbers and returns the builder for chaining.
    /// Pass multiple numbers to select several months by their numeric values when composing the expression.
    /// </summary>
    public CronBuilder InMonthNumbers(params int[] months) 
    { 
        _month.InMonthNumbers(months); 
        return this; 
    }

    /// <summary>
    /// Configures the day-of-week field to the specified <see cref="CronDayOfWeek"/> and returns the builder for chaining.
    /// Use this to target a specific weekday each week when composing the expression.
    /// </summary>
    public CronBuilder OnWeekday(CronDayOfWeek day) 
    { 
        _dayOfWeek.OnDay(day); 
        _dayOfWeekSet = true; 
        return this; 
    }

    /// <summary>
    /// Configures the day-of-week field to weekdays (Monday through Friday) and returns the builder for chaining.
    /// Use this to exclude weekend days when composing the expression.
    /// </summary>
    public CronBuilder OnWeekdays() 
    { 
        _dayOfWeek.Weekdays(); 
        _dayOfWeekSet = true; 
        return this; 
    }

    /// <summary>
    /// Configures the day-of-week field to weekend days (Saturday and Sunday) and returns the builder for chaining.
    /// Use this to only include weekends when composing the expression.
    /// </summary>
    public CronBuilder OnWeekends() 
    { 
        _dayOfWeek.Weekends(); 
        _dayOfWeekSet = true; 
        return this; 
    }

    /// <summary>
    /// Configures the day-of-week field to trigger on any of the specified <see cref="CronDayOfWeek"/> values and returns the builder for chaining.
    /// Pass multiple values to select several weekdays when composing the expression.
    /// </summary>
    public CronBuilder OnDaysOfWeek(params CronDayOfWeek[] days) 
    { 
        _dayOfWeek.OnDays(days); 
        _dayOfWeekSet = true; 
        return this; 
    }

    /// <summary>
    /// Configures the day-of-week field to include days within the inclusive range from <paramref name="start"/> to <paramref name="end"/> and returns the builder for chaining.
    /// </summary>
    public CronBuilder BetweenDaysOfWeek(CronDayOfWeek start, CronDayOfWeek end) 
    { 
        _dayOfWeek.Between(start, end); 
        _dayOfWeekSet = true; 
        return this; 
    }

    /// <summary>
    /// Quartz: Configures the day-of-week field to the nth occurrence of the specified weekday in the month and returns the builder for chaining (e.g., 2nd Monday).
    /// </summary>
    public CronBuilder OnNthWeekdayOfMonth(CronDayOfWeek day, int nth) 
    { 
        EnsureQuartz(nameof(OnNthWeekdayOfMonth)); 
        _dayOfWeek.NthOfMonth(day, nth); 
        _dayOfWeekSet = true; 
        return this; 
    }

    /// <summary>
    /// Quartz: Configures the day-of-week field to the last occurrence of the specified weekday in the month and returns the builder for chaining.
    /// </summary>
    public CronBuilder OnLastWeekday(CronDayOfWeek day) 
    { 
        EnsureQuartz(nameof(OnLastWeekday)); 
        _dayOfWeek.LastOfMonth(day); 
        _dayOfWeekSet = true; 
        return this; 
    }

    /// <summary>
    /// Creates a preset to generate a schedule triggering on the specified numeric day-of-week value (0 = Sunday).
    /// Preferred for Unix-style cron expressions where days are represented by numeric values.
    /// </summary>
    public CronBuilder OnDayOfWeekNumber(int day) 
    { 
        _dayOfWeek.OnDayNumber(day); 
        _dayOfWeekSet = true; 
        return this; 
    }

    /// <summary>
    /// Configures the day-of-week field to include numeric day-of-week values within the inclusive range from <paramref name="start"/> to <paramref name="end"/> and returns the builder for chaining.
    /// Useful for Unix-style cron expressions that use numeric day-of-week representations.
    /// </summary>
    public CronBuilder BetweenDayOfWeekNumbers(int start, int end) 
    { 
        _dayOfWeek.BetweenNumbers(start, end); 
        _dayOfWeekSet = true; 
        return this; 
    }

    /// <summary>
    /// Validates all configured presets, resolves any Quartz-specific conflicts and returns the final cron expression string in the selected format.
    /// Call this after configuring the desired schedule presets to obtain the cron expression.
    /// </summary>
    public string Build()
    {
        ResolveQuartzDayConflict();

        return _format switch
        {
            CronFormat.Unix => $"{_minute} {_hour} {_dayOfMonth} {_month} {_dayOfWeek}",
            CronFormat.Quartz => $"{_second} {_minute} {_hour} {_dayOfMonth} {_month} {_dayOfWeek}",
            _ => throw new InvalidOperationException($"Unknown format: {_format}")
        };
    }

    /// <inheritdoc cref="Build"/>
    public override string ToString() => Build();

    private void ResolveQuartzDayConflict()
    {
        if (_format != CronFormat.Quartz) return;

        if (_dayOfWeekSet && !_dayOfMonthSet)
            _dayOfMonth.Unspecified();
        else if (_dayOfMonthSet && !_dayOfWeekSet)
            _dayOfWeek.Unspecified();
        else if (!_dayOfMonthSet && !_dayOfWeekSet)
            _dayOfWeek.Unspecified();         
    }

    private void EnsureQuartz(string methodName)
    {
        if (_format != CronFormat.Quartz)
            throw new InvalidOperationException(
                $"{methodName}() is only available in Quartz format. " +
                $"Use CronExpression.Create(CronFormat.Quartz) to enable it.");
    }
}
