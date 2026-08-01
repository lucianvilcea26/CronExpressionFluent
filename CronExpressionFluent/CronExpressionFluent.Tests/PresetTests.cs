using CronExpressionFluent.Enums;
using Xunit;

namespace CronExpressionFluent.Tests;

public class PresetTests
{
    [Fact] 
    public void EveryMinute() => Assert.Equal("* * * * *", CronExpression.EveryMinute());

    [Fact] 
    public void EveryNMinutes() => Assert.Equal("*/15 * * * *", CronExpression.EveryNMinutes(15));

    [Fact] 
    public void Hourly() => Assert.Equal("0 * * * *", CronExpression.Hourly());

    [Fact] 
    public void HourlyAt() => Assert.Equal("30 * * * *", CronExpression.HourlyAt(30));

    [Fact] 
    public void Daily() => Assert.Equal("0 0 * * *", CronExpression.Daily());

    [Fact] 
    public void DailyAt() => Assert.Equal("30 9 * * *", CronExpression.DailyAt(9, 30));

    [Fact] 
    public void Weekly() => Assert.Equal("0 0 * * MON", CronExpression.Weekly(CronDayOfWeek.Monday));

    [Fact] 
    public void Monthly() => Assert.Equal("0 0 1 * *", CronExpression.Monthly());

    [Fact] 
    public void Yearly() => Assert.Equal("0 0 1 JAN *", CronExpression.Yearly());
}
