using System;
using FluentAssertions;
using Xunit;

namespace Lab01;

public class DateUtilityTests
{
    [Fact]
    public void Today_is_Payday()
    {
        // test
        var dateUtility = new FakeDateUtility();
        dateUtility.Today = new DateTime(2023, 12, 5);
        dateUtility.IsPayday().Should().BeTrue();
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