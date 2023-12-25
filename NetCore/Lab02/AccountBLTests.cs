using FluentAssertions;
using Xunit;

namespace Lab02;

public class AccountBLTests
{
    [Fact]
    public void Login_is_valid()
    {
        // test  
        var accountBL = new AccountBL();

        var isValid = accountBL.Login("cash", "cash123456");
        isValid.Should().BeTrue();
    }
}