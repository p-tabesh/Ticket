using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ticket.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ticket.Application.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("account")]
public class AccountController : Controller
{

    private readonly IConfiguration _configuration;
    private readonly AccountService _accountService;
    private readonly IDistributedCache _redisCache;
    public AccountController(IConfiguration configuration, AccountService accountService, IDistributedCache redisDistributedCache)
    {
        _configuration = configuration;
        _accountService = accountService;
        _redisCache = redisDistributedCache;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginWithCookie(string username, string password)
    {
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, username),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        await Console.Out.WriteLineAsync(HttpContext.User.Claims.ToList()[0].Value);
        return Ok();
    }

    [HttpPost]
    [Route("user-login")]
    public IActionResult LoginWithJwtToken([FromBody] LoginModel loginModel)
    {
        if (_accountService.CheckUserIdentity(loginModel.Username, loginModel.Password))
        {
            var tokenString = _accountService.GenerateToken(loginModel.Username,loginModel.Rule);
            return Ok(tokenString);
        }
        return BadRequest("password incorrect");       
    }

    [HttpGet]
    [Route("check-user")] 
    public IActionResult CheckUser([FromHeader] string Authorization)
    {
        var tokenHeader = HttpContext.Request.Headers["Authorization"];
        var tokenAuth = HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "Authorization");
        var test = HttpContext.User.Claims;
        return Ok();
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
