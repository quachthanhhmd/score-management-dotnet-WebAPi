using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using qlsv.Data;
using qlsv.Models.Interfaces;
using qlsv.Utilities.Exceptions;
using qlsv.ViewModels.Common;
using qlsv.ViewModels.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly ApplicationDbContext _context;

        public DepartmentService(
            ApplicationDbContext context
            )
        {
            _context = context;
        }


        public async Task<ApiResult<Departments>> GetDepartment(string Id)
        {
            var department = await _context.Departments.FindAsync(Id);

            if (department == null)
                return new ApiErrorResult<Departments>("Department not found");

            return new ApiSuccessResult<Departments>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = department
            };
        }

        //<summary>
        //<c><b>Input: </b> CreateDepartmentRequest</c>
        //<c><b>Output: </b> (int) = 0 if unsuccess and >0 if success</c>
        //</summary>
        public async Task<ApiResult<Departments>> CreateDepartment(CreateDepartmentRequest request)
        {
            var department = await _context.Departments.FindAsync(request.DepartmentId);

            if (department != null)
                throw new QLSVException("Department has been already");

            var newDepartment = new Departments()
            {
                DepartmentId = request.DepartmentId,
                Name = request.Name,
                LeaderId = request.leaderId
            };

            await _context.Departments.AddAsync(newDepartment);
            var result = await _context.SaveChangesAsync();


            if (result == 0)
                return new ApiErrorResult<Departments>("Create fail!!");

            return new ApiSuccessResult<Departments>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = newDepartment
            };

        }

        //<summary>
        //<c><b>Input: </b> UpdateDepartmentRequest</c>
        //<c><b>Output: </b> Departments</c>
        //</summary>
        public async Task<ApiResult<Departments>> UpdateDepartment(string Id, UpdateDepartmentRequest request)
        {
            var department = await _context.Departments.FindAsync(Id);

            if (department == null)
                return new ApiErrorResult<Departments>("Department not found.");


            department.DepartmentId = (request.DepartmentId != null) ? request.DepartmentId : department.DepartmentId;
            department.Name = (request.Name != null) ? request.Name : department.Name;
            department.LeaderId = (request.leaderId != null) ? request.leaderId : department.LeaderId;

            _context.Departments.Update(department);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return new ApiErrorResult<Departments>("Update unsuccessfully");

            return new ApiSuccessResult<Departments>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = department
            };
        }

        //<summary>
        //<c><b>Input: </b> Department Id</c>
        //<c><b>Output: </b> (int) = 0 if unsuccess and > 0 if success</c>
        //</summary>
        public async Task<ApiResult<bool>> DeleteDepartment(string Id)
        {
            var department = await _context.Departments.FindAsync(Id);

            if (department == null)
                return new ApiErrorResult<bool>("Department not found");

            _context.Departments.Remove(department);
            var result =  await _context.SaveChangesAsync();

            if (result == 0)
                return new ApiErrorResult<bool>("Delete unsuccessfully");

            return new ApiSuccessResult<bool>()
            {
                IsSuccessed = true,
                Message = "Success"
            };
        }

        public async Task<ApiResult<PageResult<DepartmentViewPaging>>> GetPagingDepartment(PagingDepartmentRequest request)
        {


            var query = from d in _context.Departments
                        join u in _context.Users on d.LeaderId equals u.Id into subID
                        from k in subID.DefaultIfEmpty()
                        select new { d, u = k };

            //filter
            if (!string.IsNullOrEmpty(request.LeaderName))
            {


                query = query.Where(x => x.u.Name == request.LeaderName);
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new DepartmentViewPaging()
                {
                    DepartmentId = x.d.DepartmentId,
                    LeaderName = x.u.Name,
                    Name = x.d.Name
                }).ToListAsync();

            // Select and projection
            var pagedResult = new PageResult<DepartmentViewPaging>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PageResult<DepartmentViewPaging>>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = pagedResult
            };
        }

    }
}
