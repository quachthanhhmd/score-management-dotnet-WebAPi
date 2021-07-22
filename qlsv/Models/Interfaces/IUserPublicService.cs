using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.ViewModels.Common;
using qlsv.Models;
using qlsv.ViewModels;
using qlsv.ViewModels.Account;
using qlsv.ViewModels.Common;
using qlsv.ViewModels.Users;

namespace qlsv.Models.Interfaces
{
    public interface IUserPublicService
    {

        Task<Users> GetOneUser(string Id);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<string>> Authencate(LoginRequest request);
        Task<ApiResult<bool>> LogOut();
        Task<ApiResult<bool>> UpdateUser(Guid Id, UpdateRequest request);
        Task<ApiResult<bool>> DeleteUser(Guid Id);
        Task<ApiResult<int>> EnrollClass(Guid Id, string ClassId);
        Task<ApiResult<int>> SendTokenToEmail(string email);
        Task<ApiResult<bool>> CheckTokenRecoveryPassword(Guid Id, string token, ResetPasswordRequest request);
        Task<ApiResult<PageResult<UserPagingView>>> GetPaging(UserPagingRequest request);

        Task<ApiResult<bool>> AssignRole(Guid Id, Guid IdRole);

    }
}
