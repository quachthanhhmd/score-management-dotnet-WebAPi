using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using qlsv.Data.Models;
using qlsv.Models.Interfaces;
using qlsv.ViewModels.Common;
using qlsv.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ApiResult<AppRole>> CreateRoles(CreateRoleRequest request)
        {
            var role = await _roleManager.FindByNameAsync(request.RoleName);
            
            if (role != null)
                return new ApiErrorResult<AppRole>("Role already exists");

            var newRole = new AppRole()
            {
                Name = request.RoleName,
                Description = request.Description
            };

            var result = await _roleManager.CreateAsync(newRole);

            if (!result.Succeeded)
                return new ApiErrorResult<AppRole>("Create Fail!!");

            return new ApiSuccessResult<AppRole>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = newRole
            };
        }

        public async Task<ApiResult<List<RoleView>>> GetAll()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleView()
                {   
                    Name = x.Name,
                    Id = x.Id
                }).ToListAsync();

            return new ApiSuccessResult<List<RoleView>>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = roles,
            };
        }
    }
}
