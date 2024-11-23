using MaterialDetails.DataContext;
using MaterialDetails.Interfaces;
using MaterialDetails.Models;
using MaterialDetails.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MaterialDetails.Services
{
    public class AuthService : IAuthService
    {
        private readonly MaterialDataContext _materialDataContext;
        private readonly PasswordHasher<User> _passwordHasher = new();
        private readonly IConfiguration _configuration;
        public AuthService(MaterialDataContext materialDataContext, PasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _materialDataContext = materialDataContext;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }
        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _materialDataContext.tbl_user.Include(x => x.Role).FirstOrDefaultAsync(u => u.Email == username);
            if (user == null) throw new Exception("Invalid credentials");
            if (VerifyPassword(user.Password, password))
            {
                return GenerateJwtToken(user);
            }
            throw new Exception("Invalid password credentials");

        }
        private bool VerifyPassword(string hasdPassword, string loginPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hasdPassword, loginPassword);
            return result == PasswordVerificationResult.Success;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings")["Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> RegisterAsync(UserSignupModel user)
        {
            var role = await _materialDataContext.tbl_role.FirstOrDefaultAsync(r => r.Name.ToLower() == user.RoleName.ToLower());
            if (role == null) throw new Exception("Role not found");
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = _passwordHasher.HashPassword(null, user.Password),
                Role = role
            };
            await _materialDataContext.tbl_user.AddAsync(newUser);
            await _materialDataContext.SaveChangesAsync();
            return true;
        }
    }
}
