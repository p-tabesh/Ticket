using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticket.Application.Extentions;
using Ticket.Domain.Entity;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;

namespace Ticket.Application.Services;

public class AccountService
{
    private readonly IConfiguration _configuration;
    private readonly TicketDbContext _dbContext;
    private readonly IDistributedCache _redisDistributedCache;

    public AccountService(IConfiguration configuration, TicketDbContext dbContext, IDistributedCache redisDistributedCache)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _redisDistributedCache = redisDistributedCache;
    }

    public string GenerateToken(int userId, string userRole)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        securityKey.KeyId = Guid.NewGuid().ToString();

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role,userRole),
            new Claim(JwtRegisteredClaimNames.NameId, Convert.ToString(userId))
        };

        var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }

    public User GetUser(string username, string password)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var user = UoW.UserRepository.GetByUsername(username.ToLower());

        if (user == null || !user.IsCorrectPassword(password.ToSha256()))
            throw new Exception("invalid username or password");

        return user;        
    }

    public void Logout(JwtSecurityToken token)
    {
        var userId = token.Claims.FirstOrDefault(t => t.Type == "nameid")?.Value;
        var jti = token.Claims.FirstOrDefault(t => t.Type == "jti")?.Value;
        _redisDistributedCache.SetString(jti,
            userId,
            new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddMinutes(10) });
    }

    public bool IsTokenBlackListed(string tokenJti)
    {
        var redis = _redisDistributedCache;
        var jti = redis.Get(tokenJti);
        return jti != null;
    }
}
