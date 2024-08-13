using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using MotorRental.Application.Interfaces;
using MotorRental.Domain.Constants;
using MotorRental.Domain.Dtos;
using MotorRental.Domain.Enums;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MotorRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApplicationControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IDeliverDriverService _deliverDriverService;
        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IDeliverDriverService deliverDriverService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _deliverDriverService = deliverDriverService;
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto registerDto)
        {
            var user = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var result = await RegisterUser(registerDto, user, MotorRentalIdentityConstants.ADMIN_ROLE_NAME);

            if (result == null || !result.Succeeded)
                return BadRequest(result.Errors.Select(a => a.Description).ToList());

            return Ok();
        }

        [HttpPost("RegisterDeliverDriver")]
        public async Task<IActionResult> RegisterDeliverDriver([FromBody] RegisterDeliverDriverDto registerDeliverDriverDto)
        {
            var user = new IdentityUser
            {
                UserName = registerDeliverDriverDto.Username,
                Email = registerDeliverDriverDto.Email
            };
            var registerDto = new RegisterDto
            {
                Password = registerDeliverDriverDto.Password,
                Username = registerDeliverDriverDto.Username,
                Email = registerDeliverDriverDto.Email,
            };

            var result = await RegisterUser(registerDto, user, MotorRentalIdentityConstants.DELIVER_DRIVER_ROLE_NAME);
            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(a => a.Description).ToList());

            await _deliverDriverService.AddAsync(new Domain.Entities.DeliverDriver
            {
                BirthDate = registerDeliverDriverDto.BirthDate,
                Email = registerDeliverDriverDto.Email,
                Cnpj = registerDeliverDriverDto.Cnpj,
                FullName = registerDeliverDriverDto.FullName,
                LicenseDriverNumber = registerDeliverDriverDto.LicenseDriverNumber,
                LicenseDriverType = registerDeliverDriverDto.LicenseDriverType,
                IdentityUserId = user.Id
            });

            return Ok();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByNameAsync(loginDto.Username);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
        }

        private async Task<IdentityResult?> RegisterUser(RegisterDto registerDto, IdentityUser user, string role)
        {
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, role);

            return result;
        }
    }
}