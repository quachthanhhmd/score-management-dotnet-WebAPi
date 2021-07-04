using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qlsv.Models.Interfaces;
using qlsv.Models;
using qlsv.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using qlsv.Utilities.Exceptions;
using qlsv.Models.Enums;
using qlsv.ViewModels.Common;
using qlsv.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;

namespace qlsv.Models.Services
{

    public class UserPublicService : IUserPublicService
    {
   
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInUser;
        private readonly IConfiguration _configuration;
        public UserPublicService(
            UserManager<Users> userManager,
            SignInManager<Users> SignInUser,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInUser = SignInUser;
            _configuration = configuration;
         }
        
        public async Task<Users> GetOneUser(string Id)
        {
            
            var user = await _userManager.FindByIdAsync(Id);

            if (user == null)
                throw new QLSVException("User not found");


            return user;

        }

        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        { 

            var loginUser = await _userManager.FindByNameAsync(request.UserName);

            if (loginUser == null) return new ApiErrorResult<string>("Invalid");

            var result = await _signInUser.PasswordSignInAsync(loginUser, request.Password, false , true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }
            var roles = await _userManager.GetRolesAsync(loginUser);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, loginUser.Email),
                new Claim(ClaimTypes.GivenName, loginUser.Id.ToString()),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
         

            var user = await _userManager.FindByNameAsync(request.userName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }


            if (request.Password != request.ConfirmPassword)
            {
                return new ApiErrorResult<bool>("Mât khẩu không khớp");
            }

            var newUser = new Users()
            {
                Dob = request.Dob,
                Email = request.Email,
                Gender = request.Genders,
                PhoneNumber = request.PhoneNumber,
                Age = request.Age,
                Name = request.Name,
                UserName = request.userName,
                PasswordHash = request.Password,
                Address = request.Address,
                StudentId = "123"
            };

            
            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

    }
}
