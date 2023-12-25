using Lab02.Model;

namespace Lab02;

public class AccountBL
{
    private readonly IAccountDao _accountDao;
    private readonly ICryptography _cryptography;
    private readonly ILog _log;

    public AccountBL()
    {
        _accountDao = new AccountDao();
        _cryptography = new Cryptography();
    }

    public AccountBL(IAccountDao accountDao, ICryptography cryptography, ILog log)
    {
        _accountDao = accountDao;
        _cryptography = cryptography;
        _log = log;
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
            if (_accountDao.GetLoginFailedCount() >= 4)
            {
                throw new LoginException($"{account} login failed more than 5 times");
            }

            _accountDao.SetLoginFailedCount(account);
            _log.Send($"{account} login failed");

            return false;
        }
    }
}