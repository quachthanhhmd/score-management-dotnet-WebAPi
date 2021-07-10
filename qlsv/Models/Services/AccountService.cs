using Microsoft.AspNetCore.Identity;
using qlsv.Data;
using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Services
{
    public class AccountService
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInUser;

        public AccountService(
                    UserManager<Users> userManager,
                    SignInManager<Users> SignInUser,
                    ApplicationDbContext context
                    )
        {
            _userManager = userManager;
            _signInUser = SignInUser;
            _context = context;

        }

        //<summary>
        //Send Mail with Token Forgot password
        //</summary>
        public async Task<ApiResult<int>> SendTokenToEmail(string email)
        {

            return new ApiSuccessResult<int>();
        }
    }
}
