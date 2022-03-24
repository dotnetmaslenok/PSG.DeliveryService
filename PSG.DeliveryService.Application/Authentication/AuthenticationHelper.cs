using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PSG.DeliveryService.Application.Authentication;

public static class AuthenticationHelper
{
    public static string SetBearerToken(IdentityUser<int> user, IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var issuer = configuration["Authentication:Bearer:Issuer"]; 
        var audience = configuration["Authentication:Bearer:Audience"];
        var secretKey = configuration["Authentication:Bearer:SecretKey"];

        var key = Encoding.UTF8.GetBytes(secretKey);

        var securityKey = new SymmetricSecurityKey(key);

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddDays(7),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}