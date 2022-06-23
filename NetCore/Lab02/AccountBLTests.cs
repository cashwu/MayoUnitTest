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
    private ILog _log;

    [SetUp]
    public void SetUp()
    {
        _cryptography = Substitute.For<ICryptography>();
        _accountDao = Substitute.For<IAccountDao>();
        _log = Substitute.For<ILog>();
        _accountBL = new AccountBL(_accountDao, _cryptography, _log);
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

        ShouldNotLog();

        ShouldNotSetFailedCount();
    }

    [Test]
    public void Login_is_invalid()
    {
        GivenMemberForLogin("cash", new Member
        {
            Password = "sha-1234"
        });

        GivenShaPassword("12345678", "sha-1234");

        LoginShouldBeInvalid("cash", "wrong password");
    }

    [Test]
    public void Login_invalid_should_log_and_set_failed_count()
    {
        GivenLoginInvalid();

        // _log.Received(1).Send("cash login failed");
        // _log.Received().Send(Arg.Any<string>());
        ShouldLog("cash", "login failed");

        ShouldSetLoginFailedCount("cash");
    }

    private void ShouldNotSetFailedCount()
    {
        _accountDao.DidNotReceiveWithAnyArgs().SetLoginFailedCount(Arg.Any<string>());
    }

    private void ShouldSetLoginFailedCount(string account)
    {
        _accountDao.Received(1).SetLoginFailedCount(account);
    }

    private void ShouldNotLog()
    {
        _log.DidNotReceiveWithAnyArgs().Send(Arg.Any<string>());
    }

    private void ShouldLog(string account, string status)
    {
        _log.Received().Send(Arg.Is<string>(s => s.Contains(account) && s.Contains(status)));
    }

    private void GivenLoginInvalid()
    {
        GivenMemberForLogin("cash", new Member
        {
            Password = "sha-1234"
        });

        GivenShaPassword("12345678", "sha-1234");

        _accountBL.Login("cash", "wrong password");
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
        _cryptography.CashSha(password).Returns(shaPassword);
    }

    private void GivenMemberForLogin(string account, Member member)
    {
        _accountDao.GetMemberForLogin(account).Returns(member);
    }
}