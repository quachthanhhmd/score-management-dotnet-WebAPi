using eShopSolution.ViewModels.Common;
using qlsv.ViewModels.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Interfaces
{
    public interface IDepartmentService
    {
        Task<Departments> GetDepartment(string Id);
        Task<int> CreateDepartment(CreateDepartmentRequest request);
        Task<Departments> UpdateDepartment(string Id, UpdateDepartmentRequest request);
        Task<int> DeleteDepartment(string Id);

        Task<PageResult<DepartmentViewPaging>> GetPagingDepartment(PagingDepartmentRequest request);

    }
}
