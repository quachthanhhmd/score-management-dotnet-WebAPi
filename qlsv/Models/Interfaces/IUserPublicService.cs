using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qlsv.Models;
using qlsv.ViewModels;
using qlsv.ViewModels.Common;

namespace qlsv.Models.Interfaces
{
    public interface IUserPublicService
    {

        Task<Users> GetOneUser(string Id);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<string>> Authencate(LoginRequest request);
        Task<ApiResult<bool>> UpdateUser(Guid Id, UpdateRequest request);
        Task<ApiResult<bool>> DeleteUser(Guid Id);
    }
}
