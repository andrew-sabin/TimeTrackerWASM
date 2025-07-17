using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimeTracker.Shared.Models.Login;

namespace TimeTrackerAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public LoginService(SignInManager<User> signInManager, IConfiguration configuration, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            if (!result.Succeeded)
            {
                return new LoginResponse(false, "Email or password is wrong.");
            }

            var user = await _userManager.FindByNameAsync(request.Username);

            if(user == null)
            {
                return new LoginResponse(false, "User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id) // Assuming a default role for simplicity
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(
                Convert.ToInt32(_configuration["JwtExpirayInDays"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtAudience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse(true, null, jwt);
        }
    }
}
