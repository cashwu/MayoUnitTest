using FluentAssertions;
using Lab02.Model;
using NUnit.Framework;

namespace Lab02;

public class AccountBLTests
{
    [Test]
    public void Login_is_valid()
    {
        var accountDao = new FakeAccountDao();

        accountDao.Member = new Member
        {
            Password = "sha-1234"
        };

        var cryptography = new FakeCryptography();
        cryptography.ShaPassword = "sha-1234";

        var accountBL = new AccountBL(accountDao, cryptography);

        var isValid = accountBL.Login("cash", "12345678");
        isValid.Should().BeTrue();
    }
}

public class FakeCryptography : ICryptography
{
    public string ShaPassword { get; set; }

    public string CashSha(string password)
    {
        return ShaPassword;
    }
}

public class FakeAccountDao : IAccountDao
{
    public Member Member { get; set; }

    public Member GetMemberForLogin(string account)
    {
        return Member;
    }

    public void SetLoginFailedCount(string account)
    {
        throw new System.NotImplementedException();
    }

    public int GetLoginFailedCount()
    {
        throw new System.NotImplementedException();
    }
}