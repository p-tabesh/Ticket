using System.Security.Claims;

namespace Ticket.Presentation.Extentions;


public static class ClaimPrincipalExtentions
{
    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userid = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userid;
    }
}

