using System;
using FluentAssertions;
using NUnit.Framework;

namespace Lab01;

[TestFixture]
public class DateUtilityTests
{
    private FakeDateUtility _dateUtility;

    [SetUp]
    public void SetUp()
    {
        _dateUtility = new FakeDateUtility();
    }

    [Test]
    public void Today_is_Payday()
    {
        GivenToday(5);
        TodayShouldBePayday();
    }

    [Test]
    public void Today_is_not_Payday()
    {
        GivenToday(6);
        TodayShouldNotBePayday();
    }

    private void TodayShouldNotBePayday()
    {
        _dateUtility.IsPayday().Should().Be(false);
    }

    private void TodayShouldBePayday()
    {
        _dateUtility.IsPayday().Should().Be(true);
    }

    private void GivenToday(int day)
    {
        _dateUtility.Today = new DateTime(2022, 6, day);
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