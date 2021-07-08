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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace qlsv.Models.Services
{

    public class UserPublicService : IUserPublicService
    {
   
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInUser;
        private readonly IConfiguration _configuration;
        
        private readonly IWebHostEnvironment _hostingEnvironment;

        
        public UserPublicService(
            UserManager<Users> userManager,
            SignInManager<Users> SignInUser,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment
            )
        {
            _userManager = userManager;
            _signInUser = SignInUser;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
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

            string uniqueFileName = null;
            if (request.Photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                request.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
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
                LocalId = request.LocalID,
                PhotoPath = uniqueFileName,
            };

            
            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<bool>> UpdateUser(Guid Id, UpdateRequest request)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());

            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }

            
            
            if (request.Photo != null)
            {

                //1. Declare 
                string uniqueFileName = null;

                //2. Create Path and Image
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                request.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                //3. Update
                user.PhotoPath = uniqueFileName;
            }

            user.Name = (request.Name == null) ? user.Name : request.Name;
            user.Age = (int)((request.Age == null) ? user.Age : request.Age);
            user.Dob = (DateTime)((request.Dob == null) ? user.Dob : request.Dob);
            user.Address = (request.Address == null) ? user.Address : request.Address;
            user.Email = (request.Email == null) ? user.Email : request.Email;
            user.PhoneNumber = (request.PhoneNumber == null) ? user.PhoneNumber : request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return new ApiErrorResult<bool>("Update không thành công");

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteUser(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());

            if (user == null)
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Delete không thành công");
        }
    }

   

}
