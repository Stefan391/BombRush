using BombRush.Common.Helpers;
using BombRush.DatabaseContext;
using BombRush.DTO.User;
using BombRush.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Cryptography;

namespace BombRush.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : BaseController
    {
        private ApplicationDbContext _context;
        private IConfiguration _appSettings;

        public UserController(ApplicationDbContext context, IConfiguration appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == req.Username);
            if (user == null)
                return BadRequest("Invalid username or password.");

            if (!VerifyPassword(req.Password, user.Password))
                return BadRequest("Invalid username or password");

            if(!CreateAccessToken(user.Id))
                return BadRequest("An error occurred while generating the access token.");

            await JWTHelper.GenerateRefreshToken(Response, user.Id, _context);

            return Ok(new { ok = true });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            req.Username = req.Username.ToLower();
            req.Email = req.Email.ToLower();

            var exists = _context.Users.Any(x => x.Username == req.Username || x.Email == req.Email);
            if (exists)
                return BadRequest("User with that username/email already exists.");

            var password = "";

            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
            {
                byte[] salt;
                generator.GetBytes(salt = new byte[16]);
                var hash = Rfc2898DeriveBytes.Pbkdf2(req.Password, salt, 100000, HashAlgorithmName.SHA256, 32);
                byte[] hashBytes = new byte[48];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 32);
                password = Convert.ToBase64String(hashBytes);
            }

            var newUser = new User
            {
                DisplayName = req.DisplayName,
                Username = req.Username,
                Email = req.Email,
                Password = password,
                RegistrationDate = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            if (!CreateAccessToken(newUser.Id))
                return BadRequest("An error occurred while generating the access token.");

            await JWTHelper.GenerateRefreshToken(Response, newUser.Id, _context);
            return Ok(new { ok = true });
        }

        [HttpGet("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            JWTHelper.DeleteTokenJWT(Response);
            await JWTHelper.RevokeUserRefreshTokens(UserId ?? 0, _context);

            return Ok(new { ok = true });
        }

        [HttpGet("refresh-token")]
        [AllowAnonymous]
        public IActionResult RefreshJWTToken()
        {
            JWTHelper.DeleteTokenJWT(Response);

            var userId = JWTHelper.RefreshTokenUserId(RefreshToken ?? "", _context);
            if (userId == null)
                return BadRequest();

            return CreateAccessToken(userId ?? 0) ? Ok() : BadRequest();
        }

        [HttpGet("refresh-info")]
        public IActionResult RefreshInfo()
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == (UserId ?? 0));
            if (user == null)
                return BadRequest();

            var res = new RefreshInfoResponse { displayName = user.DisplayName, username = user.Username, userId = user.Id };

            return Ok(res);
        }

        [HttpGet("user")]
        public IActionResult Info()
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == (UserId ?? 0));
            if (user == null)
                return BadRequest();

            var res = new InfoResponse { userId = user.Id, displayName = user.DisplayName, username = user.Username, email = user.Email };

            return Ok(res);
        }

        private bool CreateAccessToken(long userId)
        {
            var secret = _appSettings["JWTInfo:secret"];
            var issuer = _appSettings["JWTInfo:ValidIssuer"];
            var audience = _appSettings["JWTInfo:ValidAudience"];

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return false;

            if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                return false;

            JWTHelper.GenerateJWT(Response, user, secret, issuer, audience);
            return true;
        }

        private bool VerifyPassword(string password, string dbPassword)
        {
            var savedHash = dbPassword;
            byte[] hashBytes = Convert.FromBase64String(savedHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }
    }
}
