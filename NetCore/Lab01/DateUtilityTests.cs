using FluentAssertions;
using Xunit;

namespace Lab01;

public class DateUtilityTests
{
    [Fact]
    public void Today_is_Payday()
    {
        // test
        var dateUtility = new DateUtility();
        dateUtility.IsPayday().Should().BeTrue();
    }
}