using qlsv.Data;
using qlsv.Models.Interfaces;
using qlsv.ViewModels;
using qlsv.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace qlsv.Models.Services
{
    public class ClassService : IClassService
    {
        private readonly ApplicationDbContext _context;

        public ClassService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> CreateClass(AddClassRequest request)
        {
            var findClass = await _context.Class.FindAsync(request.ClassId);

            if (findClass != null)
                return new ApiErrorResult<bool>("Lớp học đã tồn tại");

            var newClass = new Class()
            {
                ClassId = request.ClassId,
                ClassName = request.ClassName,
                Capacity = request.Capacity,
                TeacherId = request.TeacherId,
                RoomId = request.RoomId
            };

            await _context.Class.AddAsync(newClass);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

       
    }
}
