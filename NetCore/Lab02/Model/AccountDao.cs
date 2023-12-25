namespace Lab02.Model;

public interface IAccountDao
{
    Member GetMemberForLogin(string account);

    void SetLoginFailedCount(string account);

    int GetLoginFailedCount();
}

public class AccountDao : IAccountDao
{
    public virtual Member GetMemberForLogin(string account)
    {
        throw new System.NotImplementedException();
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