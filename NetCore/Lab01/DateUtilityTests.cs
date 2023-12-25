using System;
using FluentAssertions;
using Xunit;

namespace Lab01;

public class DateUtilityTests
{
    private readonly FakeDateUtility _fakeDateUtility;

    public DateUtilityTests()
    {
        _fakeDateUtility = new FakeDateUtility();
    }

    [Fact]
    public void Today_is_Payday()
    {
        // test
        GivenToday(12, 5);
        TodayShouldBePayday();
    }

    private void TodayShouldBePayday()
    {
        _fakeDateUtility.IsPayday().Should().BeTrue();
    }

    private void GivenToday(int month, int day)
    {
        _fakeDateUtility.Today = new DateTime(2023, month, day);
    }
}

public class FakeDateUtility : DateUtility
{
    public DateTime Today { get; set; }

    protected override DateTime GetToday()
    {
        return Today;
    }
}