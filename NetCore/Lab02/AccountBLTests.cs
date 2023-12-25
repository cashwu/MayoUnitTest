using FluentAssertions;
using Lab02.Model;
using NSubstitute;
using Xunit;

namespace Lab02;

public class AccountBLTests
{
    [Fact]
    public void Login_is_valid()
    {
        // test  
        var accountDao = Substitute.For<IAccountDao>();

        accountDao.GetMemberForLogin("cash")
                  .Returns(new Member
                  {
                      Password = "sha-1234"
                  });

        var cryptography = Substitute.For<ICryptography>();

        cryptography.CashSha("cash123456")
                    .Returns("sha-1234");

        var accountBL = new AccountBL(accountDao, cryptography);

        var isValid = accountBL.Login("cash", "cash123456");
        isValid.Should().BeTrue();
    }
}