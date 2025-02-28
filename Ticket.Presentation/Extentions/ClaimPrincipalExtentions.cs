using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ticket.Application.Models;

namespace Ticket.Presentation.Extentions;


public static class ClaimPrincipalExtentions
{
    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userid = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userid;
    }
}

