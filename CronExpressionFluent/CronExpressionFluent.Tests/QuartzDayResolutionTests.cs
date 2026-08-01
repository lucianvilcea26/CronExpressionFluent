using CronExpressionFluent.Enums;
using Xunit;

namespace CronExpressionFluent.Tests;

public class QuartzDayResolutionTests
{
    [Fact]
    public void DowSet_DomBecomesQuestion()
    {
        var result = CronExpression.Create(CronFormat.Quartz)
            .AtSecond(0).AtMinute(0).AtHour(9)
            .OnWeekdays()
            .Build();
        Assert.Equal("0 0 9 ? * MON-FRI", result);
    }

    [Fact]
    public void DomSet_DowBecomesQuestion()
    {
        var result = CronExpression.Create(CronFormat.Quartz)
            .AtSecond(0).AtMinute(0).AtHour(0)
            .OnDay(15)
            .Build();
        Assert.Equal("0 0 0 15 * ?", result);
    }

    [Fact]
    public void NeitherSet_DowBecomesQuestion()
    {
        var result = CronExpression.Create(CronFormat.Quartz)
            .AtSecond(0).AtMinute(30).AtHour(9)
            .Build();
        Assert.Equal("0 30 9 * * ?", result);
    }
}

