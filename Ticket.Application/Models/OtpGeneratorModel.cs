namespace Ticket.Application.Models;

public class OtpGeneratorModel
{
    public string phoneNumber { get; init; }

    public int Otp
    {
        get { return Convert.ToInt32(phoneNumber.Substring(6, 5)); }

    }

    public DateTime ExpireDate { get { return DateTime.Now.AddMinutes(1); } }
}
