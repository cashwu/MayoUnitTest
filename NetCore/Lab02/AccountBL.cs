using Lab02.Model;

namespace Lab02;

public class AccountBL
{
    private readonly IAccountDao _accountDao;
    private readonly ICryptography _cryptography;

    public AccountBL(IAccountDao accountDao, ICryptography cryptography)
    {
        _accountDao = accountDao;
        _cryptography = cryptography;
    }

    public AccountBL()
    {
        _accountDao = new AccountDao();
        _cryptography = new Cryptography();
    }

    public bool Login(string account, string password)
    {
        var member = _accountDao.GetMemberForLogin(account);
        var encryptedPassword = _cryptography.CashSha(password);
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