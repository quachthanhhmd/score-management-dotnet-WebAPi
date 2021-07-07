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

        Task<int> CreateClassRoom(CreateClassRoomRequest request);
        Task<ClassRoom> UpdateClassRoom(string Id, CreateClassRoomRequest request);
        Task<int> DeleteClassRoom(string Id);
        Task<ClassRoom> GetClassRoom(string Id);
        Task<PageResult<ClassRoom>> GetPagingClasRoom(PagingClassRequest request);

    }
}
