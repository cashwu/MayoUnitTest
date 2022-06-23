using FluentAssertions;
using Lab02.Model;
using NSubstitute;
using NUnit.Framework;

namespace Lab02;

public class AccountBLTests
{
    [Test]
    public void Login_is_valid()
    {
        var accountDao = Substitute.For<IAccountDao>();

        accountDao.GetMemberForLogin("cash").Returns(new Member
        {
            Password = "sha-1234"
        });

        var cryptography = Substitute.For<ICryptography>();
        cryptography.CashSha("12345678").Returns("sha-1234");

        var accountBL = new AccountBL(accountDao, cryptography);

        var isValid = accountBL.Login("cash", "12345678");
        isValid.Should().BeTrue();
    }
}