using FluentAssertions;
using Lab02.Model;
using Xunit;

namespace Lab02;

public class AccountBLTests
{
    [Fact]
    public void Login_is_valid()
    {
        // test  
        var accountDao = new FakeAccountDao();

        accountDao.Member = new Member
        {
            Password = "sha-1234"
        };

        var cryptography = new FakeCryptography();
        cryptography.ShaPassword = "sha-1234";

        var accountBL = new AccountBL(accountDao, cryptography);

        var isValid = accountBL.Login("cash", "cash123456");
        isValid.Should().BeTrue();
    }
}

public class FakeAccountDao : AccountDao
{
    public Member Member { get; set; }

    public override Member GetMemberForLogin(string account)
    {
        return Member;
    }
}

public class FakeCryptography : Cryptography
{
    public string ShaPassword { get; set; }

    public override string CashSha(string password)
    {
        return ShaPassword;
    }
}