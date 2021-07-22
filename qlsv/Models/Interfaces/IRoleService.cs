using qlsv.Data.Models;
using qlsv.ViewModels.Common;
using qlsv.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Interfaces
{
    public interface IRoleService
    {

        public Task<ApiResult<AppRole>> CreateRoles(CreateRoleRequest request);
        public Task<ApiResult<List<RoleView>>> GetAll();
    }
}
