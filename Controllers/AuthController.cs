using Microsoft.AspNetCore.Mvc;
using SecureUserAuthAPI.Data;
using SecureUserAuthAPI.DTOs;
using SecureUserAuthAPI.Models;
using SecureUserAuthAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace SecureUserAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return BadRequest("Usuário já existe.");

            _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Usuário registrado com sucesso.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
                return BadRequest("Usuário não encontrado.");

            if (!_authService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Senha incorreta.");

            var token = _authService.CreateToken(user);

            var refreshToken = _authService.GenerateRefreshToken();
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken = token,
                refreshToken = refreshToken.Token
            });
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<string>> Refresh([FromBody] string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null || user.TokenExpires < DateTime.Now)
                return Unauthorized("Refresh token inválido ou expirado.");

            var newToken = _authService.CreateToken(user);
            var newRefresh = _authService.GenerateRefreshToken();

            user.RefreshToken = newRefresh.Token;
            user.TokenCreated = newRefresh.Created;
            user.TokenExpires = newRefresh.Expires;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken = newToken,
                refreshToken = newRefresh.Token
            });
        }

        [HttpGet("users")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
