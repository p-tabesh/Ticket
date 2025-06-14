﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Services;
using Ticket.Application.Models;
using System.IdentityModel.Tokens.Jwt;
using Ticket.Presentation.Extentions;

namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("account")]
public class AccountController : BaseController
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        var user = _accountService.GetUser(loginModel.Username, loginModel.Password);
        var tokenString = _accountService.GenerateToken(user.Id, user.IsAdmin);
        return Ok(new { Token = tokenString});
    }

    [HttpGet]
    [Route("logout")]
    public IActionResult Logout()
    {
        try
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(AuthorizationValue);
            _accountService.Logout(token);
            return Ok("logged out");
        }
        catch (Exception)
        {
            throw new Exception("token invalid");
        }
    }
}



