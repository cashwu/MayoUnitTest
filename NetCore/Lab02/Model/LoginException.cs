using System;

namespace Lab02.Model;

public class LoginException : Exception
{
    public LoginException(string message)
        : base(message)
    {
    }
}