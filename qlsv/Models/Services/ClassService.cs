using qlsv.Data;
using qlsv.Models.Interfaces;
using qlsv.Utilities.Exceptions;
using qlsv.ViewModels;
using qlsv.ViewModels.Class;
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
                RoomId = request.RoomId,
                DepartmentId = request.DepartmentId,
                NumberCredits = request.NumberCredits,
                NumberLessons = request.NumberLessons,
                Year = request.Year,
                Semester = request.Semester

            };

            await _context.Class.AddAsync(newClass);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<Class> UpdateClass(string Id, UpdateClassRequest request)
        {
            var findClass = await _context.Class.FindAsync(Id);
            if (findClass == null)
                throw new QLSVException("Class not found");

            findClass.ClassId = (request.ClassId != null) ? request.ClassId: findClass.ClassId;
            findClass.ClassName = (request.ClassName != null) ? request.ClassName : findClass.ClassName;
            findClass.Capacity = (int)((request.Capacity != null) ? request.Capacity : findClass.Capacity);
            findClass.TeacherId = (request.TeacherId != null) ? request.TeacherId : findClass.TeacherId;
            findClass.RoomId = (request.RoomId != null) ? request.RoomId : findClass.RoomId;

            findClass.DepartmentId = (request.DepartmentId != null) ? request.DepartmentId : findClass.DepartmentId;
            findClass.NumberCredits = (int)((request.NumberCredits != null) ? request.NumberCredits : findClass.NumberCredits);
            findClass.NumberLessons = (int)((request.NumberLessons != null) ? request.NumberLessons : findClass.NumberLessons);
            findClass.Year = (int)((request.Year != null) ? request.Year : findClass.Year);
            findClass.Semester = (int)((request.Semester != null) ? request.Semester : findClass.Semester);


            _context.Class.Update(findClass);
            await _context.SaveChangesAsync();

            return findClass;
        }

        public async Task<Class> GetClass(string Id)
        {
            var findClass = await _context.Class.FindAsync(Id);

            if (findClass == null)
                throw new QLSVException("Class not found");

            return findClass;
        }

        public async Task<ApiResult<bool>> DeleteClass(string Id)
        {

            var findClass = await _context.Class.FindAsync(Id);

            if (findClass == null)
                return new ApiErrorResult<bool>("Class not found");

            var result =  _context.Class.Remove(findClass);

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }
    }
}
