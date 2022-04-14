using Lab02.Model;

namespace Lab02;

public class AccountBL
{
    public bool Login(string account, string password)
    {
        var accountDao = new AccountDao();

        var member = accountDao.GetMemberForLogin(account);
        var encryptedPassword = new Cryptography().CashSha(password);
        var isValid = member.Password == encryptedPassword;

        if (isValid)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}