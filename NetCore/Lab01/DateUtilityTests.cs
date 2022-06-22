using System;
using FluentAssertions;
using NUnit.Framework;

namespace Lab01;

[TestFixture]
public class DateUtilityTests
{
    [Test]
    public void Today_is_Payday()
    {
        var dateUtility = new FakeDateUtility();
        dateUtility.Today = new DateTime(2022, 6, 5);
        dateUtility.IsPayday().Should().Be(true);
    }
}

public class FakeDateUtility : DateUtility
{
    public DateTime Today { get; set; }

    protected override DateTime GivenToday()
    {
        return Today;
    }
}