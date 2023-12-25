using FluentAssertions;
using Lab02.Model;
using NSubstitute;
using Xunit;

namespace Lab02;

public class AccountBLTests
{
    private readonly IAccountDao _accountDao;
    private readonly ICryptography _cryptography;
    private readonly AccountBL _accountBL;

    public AccountBLTests()
    {
        _accountDao = Substitute.For<IAccountDao>();
        _cryptography = Substitute.For<ICryptography>();
        _accountBL = new AccountBL(_accountDao, _cryptography);
    }

    [Fact]
    public void Login_is_valid()
    {
        // test  
        GivenMemberForLogin("cash", new Member
        {
            Password = "sha-1234"
        });

        GivenShaPassword("cash123456", "sha-1234");

        LoginShouldBeValid("cash", "cash123456");
    }

    private void LoginShouldBeValid(string account, string password)
    {
        var isValid = _accountBL.Login(account, password);
        isValid.Should().BeTrue();
    }

    private void GivenShaPassword(string password, string shaPassword)
    {
        _cryptography.CashSha(password)
                     .Returns(shaPassword);
    }

    private void GivenMemberForLogin(string account, Member member)
    {
        _accountDao.GetMemberForLogin(account)
                   .Returns(member);
    }
}