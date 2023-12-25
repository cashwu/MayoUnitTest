namespace Lab02.Model;

public interface ICryptography
{
    string CashSha(string password);
}

public class Cryptography : ICryptography
{
    public string CashSha(string password)
    {
        throw new System.NotImplementedException();
    }
}