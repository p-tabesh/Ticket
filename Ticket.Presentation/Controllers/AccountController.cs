using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Services;
using Ticket.Application.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Caching.Distributed;

namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("account")]
public class AccountController : Controller
{

    private readonly AccountService _accountService;
    public AccountController(IConfiguration configuration, AccountService accountService, IDistributedCache redisDistributedCache)
    {
        _accountService = accountService;
    }


    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        var user = _accountService.GetUser(loginModel.Username, loginModel.Password);
        var tokenString = _accountService.GenerateToken(user.Id, loginModel.Rule);
        return Ok(tokenString);
    }


    [Authorize]
    [Authorize(Policy = "Admin")]
    [HttpGet]
    [Route("logout")]
    public IActionResult Logout()
    {
        try
        {
            string authorization = HttpContext.Request.Headers["Authorization"];
            var token = new JwtSecurityTokenHandler().ReadJwtToken(authorization[7..]);
            _accountService.Logout(token);
            return Ok("[not really] logged out");
        }
        catch (Exception)
        {
            throw new Exception("token invalid");
        }
    }
}



