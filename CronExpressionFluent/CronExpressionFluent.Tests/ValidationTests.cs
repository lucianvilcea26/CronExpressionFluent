using CronExpressionFluent.Enums;
using CronExpressionFluent.Exceptions;
using Xunit;

namespace CronExpressionFluent.Tests;

public class ValidationTests
{
    [Fact]
    public void MinuteOutOfRange_Throws() =>
        Assert.Throws<InvalidCronFieldException>(() =>
            CronExpression.Create().AtMinute(60).Build());

    [Fact]
    public void HourOutOfRange_Throws() =>
        Assert.Throws<InvalidCronFieldException>(() =>
            CronExpression.Create().AtHour(25).Build());

    [Fact]
    public void NegativeStep_Throws() =>
        Assert.Throws<InvalidCronFieldException>(() =>
            CronExpression.Create().EveryMinutes(-1).Build());

    [Fact]
    public void ZeroStep_Throws() =>
        Assert.Throws<InvalidCronFieldException>(() =>
            CronExpression.Create().EveryMinutes(0).Build());

    [Fact]
    public void DayOutOfRange_Throws() =>
        Assert.Throws<InvalidCronFieldException>(() =>
            CronExpression.Create().OnDay(32).Build());

    [Fact]
    public void DayZero_Throws() =>
        Assert.Throws<InvalidCronFieldException>(() =>
            CronExpression.Create().OnDay(0).Build());

    [Fact]
    public void SecondOnUnix_Throws() =>
        Assert.Throws<InvalidOperationException>(() =>
            CronExpression.Create(CronFormat.Unix).AtSecond(0).Build());

    [Fact]
    public void NthWeekdayOnUnix_Throws() =>
        Assert.Throws<InvalidOperationException>(() =>
            CronExpression.Create(CronFormat.Unix).OnNthWeekdayOfMonth(CronDayOfWeek.Monday, 2).Build());

    [Fact]
    public void NthOutOfRange_Throws() =>
        Assert.Throws<InvalidCronFieldException>(() =>
            CronExpression.Create(CronFormat.Quartz).OnNthWeekdayOfMonth(CronDayOfWeek.Monday, 6).Build());

    [Fact]
    public void MonthOutOfRange_Throws() =>
        Assert.Throws<InvalidCronFieldException>(() =>
            CronExpression.Create().InMonthNumber(13).Build());
}
