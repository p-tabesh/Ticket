using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Ticket.Application.Services;

namespace Ticket.Presentation.Middlewares;

public class JwtMiddleware : IMiddleware
{
    public readonly AccountService _accountService;
    public JwtMiddleware(AccountService accountService)
    {
        _accountService = accountService;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (IsUnAuthorizeEndpoint(context.GetEndpoint()) || context.Request.Path.ToString().Contains("swagger")) 
        {
            await next(context);
            return;
        }
        
        if (TokenIsValid(context.Request.Headers["Authorization"]))
        {
            await next(context);
            return;
        }

        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("token invalid");
        return;


    }


    private bool IsUnAuthorizeEndpoint(Endpoint endpoint)
    {
        var authorizeAttribute = endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>();
        if (authorizeAttribute == null)
        {
            return false;
        }
        return true;
    }

    private bool TokenIsValid(string authorizationValue)
    {
        try
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(authorizationValue[7..]);
            var jti = token.Claims.FirstOrDefault(t => t.Type == "jti")?.Value;
            if (jti != null && _accountService.IsTokenBlackListed(jti))
            {
                return false;
            }
            return true;
        }
        catch (Exception)
        {
            throw new Exception("token invalid");
        }
    }
}
