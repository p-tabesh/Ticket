using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticket.Application.Extentions;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;

namespace Ticket.Application.Services;

public class AccountService
{
    private readonly IConfiguration _configuration;
    private readonly TicketDbContext _dbContext;
    public AccountService(IConfiguration configuration, TicketDbContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }
    public string GenerateToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        securityKey.KeyId = Guid.NewGuid().ToString();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,username),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires:DateTime.Now.AddMinutes(10),
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }

    public bool CheckUserIdentity(string username, string password)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var user = UoW.UserRepository.GetByUsername(username.ToLower());

        if (user == null)
            throw new Exception("user doesnt exists");

        if (user.IsCorrectPassword(password.ToSha256()))
            return true;

        return false;
    }
}
