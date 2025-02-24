using Microsoft.Extensions.Caching.Distributed;
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
    private readonly IDistributedCache _redisDistributedCache;
    private static readonly string blackListKey = "tokenBlackList";
    public AccountService(IConfiguration configuration, TicketDbContext dbContext, IDistributedCache redisDistributedCache)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _redisDistributedCache = redisDistributedCache;
    }
    public string GenerateToken(string username, string userRole)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        securityKey.KeyId = Guid.NewGuid().ToString();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,username),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role,userRole)
        };
        
        var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
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
    public void Logout(JwtSecurityToken token)
    {
        var username = token.Claims.FirstOrDefault(t => t.Type == "sub")?.Value;
        var jti = token.Claims.FirstOrDefault(t => t.Type == "jti")?.Value;
        _redisDistributedCache.SetString(jti,
            username,
            new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddMinutes(10) });
    }

    public bool IsTokenBlackListed(string tokenJti)
    {
        var redis = _redisDistributedCache;
        var jti = redis.Get(tokenJti);
        return jti != null;
    }
}
