using FluentAssertions;
using Lab02.Model;
using NSubstitute;
using NUnit.Framework;

namespace Lab02;

public class AccountBLTests
{
    private IAccountDao _accountDao;
    private ICryptography _cryptography;
    private AccountBL _accountBL;

    [SetUp]
    public void SetUp()
    {
        _cryptography = Substitute.For<ICryptography>();
        _accountDao = Substitute.For<IAccountDao>();
        _accountBL = new AccountBL(_accountDao, _cryptography);
    }

    [Test]
    public void Login_is_valid()
    {
        GivenMemberForLogin("cash", new Member
        {
            Password = "sha-1234"
        });

        GivenShaPassword("12345678", "sha-1234");

        LoginShouldBeValid("cash", "12345678");
    }

    private void LoginShouldBeValid(string account, string password)
    {
        var isValid = _accountBL.Login(account, password);
        isValid.Should().BeTrue();
    }

    private void GivenShaPassword(string password, string shaPassword)
    {
        _cryptography.CashSha(password).Returns(shaPassword);
    }

    private void GivenMemberForLogin(string account, Member member)
    {
        _accountDao.GetMemberForLogin(account).Returns(member);
    }
}