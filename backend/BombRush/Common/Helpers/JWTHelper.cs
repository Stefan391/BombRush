using BombRush.DatabaseContext;
using BombRush.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BombRush.Common.Helpers
{
    public static class JWTHelper
    {
        public static void GenerateJWT(HttpResponse response, User user, string secret, string audience, string issuer)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(10), signingCredentials: cred, audience: audience, issuer: issuer);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            SecureCookieHelper.AppendSecureCookie(response, "TokenJWT", jwt, DateTime.UtcNow.AddMinutes(10));
        }

        public static long? RefreshTokenUserId(string token, ApplicationDbContext _context)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var refreshToken = _context.RefreshTokens.FirstOrDefault(x => !x.Revoked && x.Id.ToString() == token && x.Expires > DateTime.UtcNow);
            if (refreshToken == null)
                return null;

            return refreshToken.UserId;
        }

        public static void DeleteTokenJWT(HttpResponse response)
        {
            SecureCookieHelper.DeleteSecureCookie(response, "TokenJWT");
        }

        public static async Task GenerateRefreshToken(HttpResponse response, long userId, ApplicationDbContext _context)
        {
            await RevokeUserRefreshTokens(userId, _context);

            var newToken = new RefreshToken { Id = Guid.NewGuid(), UserId = userId, Revoked = false, Expires = DateTime.UtcNow.AddDays(7) };
            _context.RefreshTokens.Add(newToken);
            await _context.SaveChangesAsync();
            SecureCookieHelper.AppendSecureCookie(response, "RefreshToken", newToken.Id.ToString(), DateTime.UtcNow.AddDays(7));
        }

        public static async Task RevokeUserRefreshTokens(long userId, ApplicationDbContext _context)
        {
            var toBeRevoked = _context.RefreshTokens.Where(x => x.UserId == userId);

            foreach (var token in toBeRevoked)
            {
                token.Revoked = true;
            }

            _context.UpdateRange(toBeRevoked);
            await _context.SaveChangesAsync();
        }

        public static async Task RevokeRefreshToken(string refreshToken, ApplicationDbContext _context)
        {
            var token = _context.RefreshTokens.FirstOrDefault(x => x.Id.ToString() == refreshToken);
            if (token == null)
                return;

            token.Revoked = true;
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}
