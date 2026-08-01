using CronExpressionFluent.Enums;
using Xunit;

namespace CronExpressionFluent.Tests;

public class BenchmarkTests
{
    [Fact]
    public void Spec01_EveryMinute_Unix() =>
    Assert.Equal("* * * * *", CronExpression.Create().EveryMinute().Build());

    [Fact]
    public void Spec02_Every5Minutes_Unix() =>
        Assert.Equal("*/5 * * * *", CronExpression.Create().EveryMinutes(5).Build());

    [Fact]
    public void Spec03_EveryHourAtMinute0_Unix() =>
        Assert.Equal("0 * * * *", CronExpression.Create().AtMinute(0).Build());

    [Fact]
    public void Spec04_DailyAtMidnight_Unix() =>
        Assert.Equal("0 0 * * *", CronExpression.Create().AtMinute(0).AtHour(0).Build());

    [Fact]
    public void Spec05_DailyAt930_Unix() =>
        Assert.Equal("30 9 * * *", CronExpression.Create().AtMinute(30).AtHour(9).Build());

    [Fact]
    public void Spec06_Every15Minutes_Unix() =>
        Assert.Equal("*/15 * * * *", CronExpression.Create().EveryMinutes(15).Build());

    [Fact]
    public void Spec07_Every30Seconds_Quartz() =>
        Assert.Equal("*/30 * * * * ?", CronExpression.Create(CronFormat.Quartz).EverySeconds(30).Build());

    [Fact]
    public void Spec08_DailyAt6PM_Unix() =>
        Assert.Equal("0 18 * * *", CronExpression.Create().AtMinute(0).AtHour(18).Build());

    [Fact]
    public void Spec09_Every2Hours_Unix() =>
        Assert.Equal("0 */2 * * *", CronExpression.Create().AtMinute(0).EveryHours(2).Build());

    [Fact]
    public void Spec10_DailyAtNoon_Unix() =>
        Assert.Equal("0 12 * * *", CronExpression.Create().AtMinute(0).AtHour(12).Build());

    [Fact]
    public void Spec11_EveryMondayAt8_Quartz() =>
        Assert.Equal("0 0 8 ? * MON",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(8).OnWeekday(CronDayOfWeek.Monday).Build());

    [Fact]
    public void Spec12_WeekdaysAt9_Quartz() =>
        Assert.Equal("0 0 9 ? * MON-FRI",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(9).OnWeekdays().Build());

    [Fact]
    public void Spec13_FirstOfMonthMidnight_Quartz() =>
        Assert.Equal("0 0 0 1 * ?",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(0).OnDay(1).Build());

    [Fact]
    public void Spec14_WeekendsAt10_Quartz() =>
        Assert.Equal("0 0 10 ? * SAT,SUN",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(10).OnWeekends().Build());

    [Fact]
    public void Spec15_Every10MinBusinessHours_Quartz() =>
        Assert.Equal("0 */10 9-17 * * ?",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).EveryMinutes(10).BetweenHours(9, 17).Build());

    [Fact]
    public void Spec16_MWF_730_Quartz() =>
        Assert.Equal("0 30 7 ? * MON,WED,FRI",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(30).AtHour(7)
                .OnDaysOfWeek(CronDayOfWeek.Monday, CronDayOfWeek.Wednesday, CronDayOfWeek.Friday).Build());

    [Fact]
    public void Spec17_Jan1Midnight_Quartz() =>
        Assert.Equal("0 0 0 1 JAN ?",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(0).OnDay(1).InMonth(Month.January).Build());

    [Fact]
    public void Spec18_QuarterlyAt6AM_Quartz() =>
        Assert.Equal("0 0 6 1 1,4,7,10 ?",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(6).OnDay(1).InMonthNumbers(1, 4, 7, 10).Build());

    [Fact]
    public void Spec19_15thAtNoon_Quartz() =>
        Assert.Equal("0 0 12 15 * ?",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(12).OnDay(15).Build());

    [Fact]
    public void Spec20_SundayAt2359_Quartz() =>
        Assert.Equal("0 59 23 ? * SUN",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(59).AtHour(23).OnWeekday(CronDayOfWeek.Sunday).Build());

    [Fact]
    public void Spec21_LastDayOfMonth5PM_Quartz() =>
        Assert.Equal("0 0 17 L * ?",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(17).OnLastDayOfMonth().Build());

    [Fact]
    public void Spec22_LastFridayAt5PM_Quartz() =>
        Assert.Equal("0 0 17 ? * FRIL",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(17).OnLastWeekday(CronDayOfWeek.Friday).Build());

    [Fact]
    public void Spec23_2ndMondayAt10_Quartz() =>
        Assert.Equal("0 0 10 ? * MON#2",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(10).OnNthWeekdayOfMonth(CronDayOfWeek.Monday, 2).Build());

    [Fact]
    public void Spec24_NearestWeekdayTo15thAt9_Quartz() =>
        Assert.Equal("0 0 9 15W * ?",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(9).OnWeekdayNearestTo(15).Build());

    [Fact]
    public void Spec25_MarSep_MWF_630_Quartz() =>
        Assert.Equal("0 30 6 ? MAR-SEP MON,WED,FRI",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(30).AtHour(6)
                .BetweenMonths(Month.March, Month.September)
                .OnDaysOfWeek(CronDayOfWeek.Monday, CronDayOfWeek.Wednesday, CronDayOfWeek.Friday).Build());

    [Fact]
    public void Spec26_Every45SecBusinessHoursWeekdays_Quartz() =>
        Assert.Equal("*/45 * 9-17 ? * MON-FRI",
            CronExpression.Create(CronFormat.Quartz).EverySeconds(45).BetweenHours(9, 17).OnWeekdays().Build());

    [Fact]
    public void Spec27_3rdThursdayNovAt8_Quartz() =>
        Assert.Equal("0 0 8 ? NOV THU#3",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(8)
                .InMonth(Month.November).OnNthWeekdayOfMonth(CronDayOfWeek.Thursday, 3).Build());

    [Fact]
    public void Spec28_LastWeekdayOfMonth6PM_Quartz() =>
        Assert.Equal("0 0 18 LW * ?",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(18).OnLastWeekdayOfMonth().Build());

    [Fact]
    public void Spec29_1stMondayAt9_Quartz() =>
        Assert.Equal("0 0 9 ? * MON#1",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(9).OnNthWeekdayOfMonth(CronDayOfWeek.Monday, 1).Build());

    [Fact]
    public void Spec30_BiAnnual_Jun15Dec15_Noon_Quartz() =>
        Assert.Equal("0 0 12 15 6,12 ?",
            CronExpression.Create(CronFormat.Quartz).AtSecond(0).AtMinute(0).AtHour(12).OnDay(15).InMonthNumbers(6, 12).Build());
}
