namespace Ticket.Application.Utilities;

static public class PasswordChecker
{
    static public bool IsSecure(string password)
    {
        if ((password.Contains('@') || password.Contains('#')) && password.Length >= 8)
            return true;

        return false;
    }
}
