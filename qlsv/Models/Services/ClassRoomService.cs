using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using qlsv.Data;
using qlsv.Models.Interfaces;
using qlsv.Utilities.Exceptions;
using qlsv.ViewModels.ClassRoom;
using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Services
{
    public class ClassRoomService : IClassRoomService
    {

        private readonly ApplicationDbContext _context;
        
        public ClassRoomService(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public async Task<ApiResult<ClassRoom>> CreateClassRoom(CreateClassRoomRequest request)
        {
            var classRoom = await _context.classRoom.FindAsync(request.RoomID);

            if (classRoom != null)
                return new ApiErrorResult<ClassRoom>("Lớp học không tồn tại.");

            var newClassRoom = new ClassRoom()
            {
                RoomId = request.RoomID,
                Desks = request.Desks,
                Seats = request.Seats
            };

            await _context.AddAsync(newClassRoom);

            var result =  await _context.SaveChangesAsync();

            if (result == 0)
                return new ApiErrorResult<ClassRoom>("Tạo lớp học thất bại.");

            return new ApiSuccessResult<ClassRoom>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = newClassRoom
            };
        }

        public async Task<ApiResult<ClassRoom>> UpdateClassRoom(string Id, CreateClassRoomRequest request)
        {
            var classRoom = await _context.classRoom.FindAsync(Id);

            if (classRoom == null)
                return new ApiErrorResult<ClassRoom>("ClassRoom not found");

            classRoom.RoomId = (request.RoomID != null) ? request.RoomID : classRoom.RoomId;
            classRoom.Desks = (request.Desks != null) ? request.Desks : classRoom.Desks;
            classRoom.Seats = (request.Seats != null) ? request.Seats : classRoom.Seats;
            classRoom.Building = (request.Building != null) ? request.Building : classRoom.Building;

            _context.classRoom.Update(classRoom);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                return new ApiErrorResult<ClassRoom>("Update ClassRoom does not success");

            return new ApiSuccessResult<ClassRoom>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = classRoom
            };

        }

        public async Task<ApiResult<bool>> DeleteClassRoom(string Id)
        {
            var classRoom = await _context.classRoom.FindAsync(Id);

            if (classRoom == null)
                return new ApiErrorResult<bool>("ClassRoom not found");

            _context.classRoom.Remove(classRoom);


            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return new ApiErrorResult<bool>("Delete Failed");

            return new ApiSuccessResult<bool>()
            {
                IsSuccessed = true,
                Message = "Success"
            };
        }

        public async Task<ApiResult<ClassRoom>> GetClassRoom(string Id)
        {
            var classRoom = await _context.classRoom.FindAsync(Id);

            if (classRoom == null)
                return new ApiErrorResult<ClassRoom>("ClassRoom not found");

            return new ApiSuccessResult<ClassRoom>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = classRoom
            };
        }

       public async Task<ApiResult<PageResult<ClassRoom>>> GetPagingClasRoom(PagingClassRequest request)
       {
            var query = from clr in _context.classRoom
                        select new { clr };

            if (!string.IsNullOrEmpty(request.Building))
            {
                query = query.Where(x => x.clr.Building == request.Building);
            }
                        
            if (request.Desks != null)
            {
                query = query.Where(x => x.clr.Desks == request.Desks);

            }

            if (request.Seats != null)
            {
                query = query.Where(x => x.clr.Seats == request.Seats);

            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ClassRoom()
                {
                    RoomId = x.clr.RoomId,
                    Desks = x.clr.Desks,
                    Seats = x.clr.Seats,
                    Building = x.clr.Building
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PageResult<ClassRoom>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PageResult<ClassRoom>>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = pagedResult
            };
        }
    }
}
