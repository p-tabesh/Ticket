using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ticket.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ticket.Application.Models;

namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("account")]
public class AccountController : Controller
{

    private readonly IConfiguration _configuration;
    private readonly AccountService _accountService;
    public AccountController(IConfiguration configuration, AccountService accountService)
    {
        _configuration = configuration;
        _accountService = accountService;
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
            var tokenString = _accountService.GenerateToken(loginModel.Username);
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
    [HttpGet]
    [Route("logout")]
    public IActionResult Logout()
    {
        var context = HttpContext;
        return Ok("logged out");
    }
}
