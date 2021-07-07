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

        public async Task<int> CreateClassRoom(CreateClassRoomRequest request)
        {
            var classRoom = await _context.classRoom.FindAsync(request.RoomID);

            if (classRoom != null)
                throw new QLSVException("ClassRoom has existed");

            var newClassRoom = new ClassRoom()
            {
                RoomId = request.RoomID,
                Desks = request.Desks,
                Seats = request.Seats
            };

            var result = await _context.AddAsync(newClassRoom);

            return await _context.SaveChangesAsync();
        }

        public async Task<ClassRoom> UpdateClassRoom(string Id, CreateClassRoomRequest request)
        {
            var classRoom = await _context.classRoom.FindAsync(Id);

            if (classRoom == null)
                throw new QLSVException("ClassRoom not found");

            classRoom.RoomId = (request.RoomID != null) ? request.RoomID : classRoom.RoomId;
            classRoom.Desks = (request.Desks != null) ? request.Desks : classRoom.Desks;
            classRoom.Seats = (request.Seats != null) ? request.Seats : classRoom.Seats;
            classRoom.Building = (request.Building != null) ? request.Building : classRoom.Building;

            _context.classRoom.Update(classRoom);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new QLSVException("Update ClassRoom does not success");

            return classRoom;

        }

        public async Task<int> DeleteClassRoom(string Id)
        {
            var classRoom = await _context.classRoom.FindAsync(Id);

            if (classRoom == null)
                throw new QLSVException("ClassRoom not found");

            _context.classRoom.Remove(classRoom);

            return await _context.SaveChangesAsync();

        }

        public async Task<ClassRoom> GetClassRoom(string Id)
        {
            var classRoom = await _context.classRoom.FindAsync(Id);

            if (classRoom == null)
                throw new QLSVException("ClassRoom not found");

            return classRoom;
        }

       public async Task<PageResult<ClassRoom>> GetPagingClasRoom(PagingClassRequest request)
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

            return pagedResult;
        }
    }
}
