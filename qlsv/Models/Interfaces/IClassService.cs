using qlsv.ViewModels;
using qlsv.ViewModels.Class;
using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Interfaces
{
    public interface IClassService
    {
        Task<ApiResult<bool>> CreateClass(AddClassRequest request);

        Task<ApiResult<Class>> GetClass(string Id);
        Task<ApiResult<bool>> DeleteClass(string Id);
        Task<ApiResult<Class>> UpdateClass(string Id, UpdateClassRequest request);
    }
}
