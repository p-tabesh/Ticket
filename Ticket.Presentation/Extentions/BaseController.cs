using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ticket.Application.Models;

namespace Ticket.Presentation.Extentions;

public class BaseController : ControllerBase
{
    public int RequestUserId
    {
        get
        {
            return Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
    public string AuthorizationValue
    {
        get
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            return authorizationHeader[7..];
        }
    }

    public override OkObjectResult Ok(object? value)
    {
        if (value == null)
        {
            return new OkObjectResult(new ResponseBaseModel());
        }
        return base.Ok(value);
    }
}