using eShopSolution.ViewModels.Common;
using qlsv.ViewModels.ClassRoom;
using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Interfaces
{
    public interface IClassRoomService
    {

        Task<ApiResult<ClassRoom>> CreateClassRoom(CreateClassRoomRequest request);
        Task<ApiResult<ClassRoom>> UpdateClassRoom(string Id, CreateClassRoomRequest request);
        Task<ApiResult<bool>> DeleteClassRoom(string Id);
        Task<ApiResult<ClassRoom>> GetClassRoom(string Id);
        Task<ApiResult<PageResult<ClassRoom>>> GetPagingClasRoom(PagingClassRequest request);

    }
}
