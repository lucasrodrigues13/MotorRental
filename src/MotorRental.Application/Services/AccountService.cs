//using Microsoft.AspNet.Identity;
//using MotorRental.Domain.Dtos;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MotorRental.Application.Services
//{
//    public class AccountService
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        private readonly RoleManager<ApplicationRole> _roleManager;
//        private readonly IConfiguration _configuration;

//        public AccountService(
//            UserManager<ApplicationUser> userManager,
//            SignInManager<ApplicationUser> signInManager,
//            RoleManager<ApplicationRole> roleManager,
//            IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _roleManager = roleManager;
//            _configuration = configuration;
//        }
//        public async Task<bool> Register(RegisterDto registerDto)
//        {
//            var user = new IdentityUser { UserName = registerDto.Username };
//            var result = await _userManager.CreateAsync(user, registerDto.Password);

//            if (!result.Succeeded)
//            {
//                return BadRequest(result.Errors);
//            }

//            if (!await _roleManager.RoleExistsAsync(registerDto.Role))
//            {
//                await _roleManager.CreateAsync(new IdentityRole { Name = registerDto.Role });
//            }

//            await _userManager.AddToRoleAsync(user, registerDto.Role);

//            return Ok("User registered successfully");
//        }
//    }
//}
