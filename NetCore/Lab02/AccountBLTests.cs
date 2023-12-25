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
    private readonly ILog _log;

    public AccountBLTests()
    {
        _accountDao = Substitute.For<IAccountDao>();
        _cryptography = Substitute.For<ICryptography>();
        _log = Substitute.For<ILog>();
        _accountBL = new AccountBL(_accountDao, _cryptography, _log);
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

    [Fact]
    public void Login_is_invalid()
    {
        // test  
        GivenMemberForLogin("cash", new Member
        {
            Password = "sha-1234"
        });

        GivenShaPassword("cash123456", "sha-1234");

        LoginShouldBeInvalid("cash", "wrong password");
    }

    [Fact]
    public void Login_invalid_should_log()
    {
        // test  
        GivenLoginInvalid();

        // _log.Received().Send(Arg.Any<string>());
        // _log.Received(1).Send("cash login failed");

        ShouldLog("cash", "login failed");
    }

    private void ShouldLog(string account, string loginStatus)
    {
        _log.Received(1)
            .Send(Arg.Is<string>(s => s.Contains(account) && s.Contains(loginStatus)));
    }

    private void GivenLoginInvalid()
    {
        GivenMemberForLogin("cash", new Member
        {
            Password = "sha-1234"
        });

        GivenShaPassword("cash123456", "sha-1234");

        LoginShouldBeInvalid("cash", "wrong password");
    }

    private void LoginShouldBeInvalid(string account, string password)
    {
        var isValid = _accountBL.Login(account, password);
        isValid.Should().BeFalse();
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