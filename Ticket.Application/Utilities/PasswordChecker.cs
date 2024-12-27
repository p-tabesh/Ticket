namespace Ticket.Application.Utilities;

static public class PasswordChecker
{
    static public bool IsSecure(string password)
    {
        if (password == null)
        {
            throw new ArgumentNullException("password");
        }
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException();
        }
        if (password.Contains('@') || password.Contains('#'))
        {
            return true;
        }
        return false;
    }
}
