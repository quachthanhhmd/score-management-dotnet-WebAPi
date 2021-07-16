using eShopSolution.ViewModels.Common;
using qlsv.ViewModels.Common;
using qlsv.ViewModels.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Interfaces
{
    public interface IDepartmentService
    {
        Task<ApiResult<Departments>> GetDepartment(string Id);
        Task<ApiResult<Departments>> CreateDepartment(CreateDepartmentRequest request);
        Task<ApiResult<Departments>> UpdateDepartment(string Id, UpdateDepartmentRequest request);
        Task<ApiResult<bool>> DeleteDepartment(string Id);

        Task<ApiResult<PageResult<DepartmentViewPaging>>> GetPagingDepartment(PagingDepartmentRequest request);

    }
}
