namespace Lab02.Model;

public interface ICryptography
{
    string CashSha(string password);
}

public class Cryptography : ICryptography
{
    public virtual string CashSha(string password)
    {
        throw new System.NotImplementedException();
    }
}