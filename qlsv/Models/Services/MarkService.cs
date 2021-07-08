using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using qlsv.Data;
using qlsv.Models.Interfaces;
using qlsv.Utilities.Exceptions;
using qlsv.ViewModels;
using qlsv.ViewModels.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Services
{
    public class MarkService : IMarkService
    {
        private readonly ApplicationDbContext _context;

        public MarkService(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        public async Task<Marks> GetMark(string SubjectId, string UserId)
        {

            var mark = await _context.Marks.FindAsync( SubjectId, UserId);

            if (mark == null)
                throw new QLSVException("Mark not found");

            return mark;
        }

        public async Task<int> CreateMark(CreateMarkRequest request)
        {
            var mark = await _context.Marks.FindAsync(request.SubjectId, request.StudentID );

            if (mark != null)
                throw new QLSVException("Mark has ben already");

            var newMark = new Marks()
            {
                SubjectId = request.SubjectId,
                localId = request.StudentID,
                marks = request?.marks
            };

            await _context.Marks.AddAsync(newMark);
            return await _context.SaveChangesAsync();
        }

        public async Task<Marks> UpdateMark(string SubjectId, string UserId, UpdateMarkRequest request)
        {
            var mark = await _context.Marks.FindAsync( request.SubjectId, request.UserId );

            if (mark == null)
                throw new QLSVException("Mark not found");

            mark.SubjectId = string.IsNullOrEmpty(request.SubjectId) ? mark.SubjectId : request.SubjectId;
            mark.localId = string.IsNullOrEmpty(request.UserId) ? mark.localId : request.UserId;
            mark.marks = (request.marks == null) ? mark.marks : request.marks;

            _context.Marks.Update(mark);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
                throw new QLSVException("Update unsuccessfully");

            return mark;
        }

        public async Task<int> DeleteMark(string SubjectId, string UserId)
        {
            var mark = await _context.Marks.FindAsync(SubjectId, UserId );

            if (mark == null)
                throw new QLSVException("Mark not found");

            _context.Marks.Remove(mark);

            return await _context.SaveChangesAsync();
        }
        

        public async Task<PageResult<MarkViewPaging>> GetPagingMark(PagingMarkRequest request)
        {
            var query = from m in _context.Marks
                        join c in _context.Class on m.SubjectId equals c.ClassId into subIDClass
                        from ClassID in subIDClass.DefaultIfEmpty()
                        join u in _context.Users on m.localId equals u.LocalId into uuser
                        from ui in uuser.DefaultIfEmpty()
                        select new { m, c = ClassID, u = ui};

            if (request.UserID != null)
            {
                query = query.Where(x => x.u.LocalId == request.UserID);
            }
                          
            if (!string.IsNullOrEmpty(request.SubjectId))
            {
                query = query.Where(x => x.m.SubjectId == request.SubjectId);
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new MarkViewPaging()
                {
                    ClassId = x.c.ClassId,
                    SubjectName = x.c.ClassName,
                    StudentId = x.u.LocalId,
                    NumberCredits = x.c.NumberCredits,
                    StudentName = x.u.Name
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PageResult<MarkViewPaging>()
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
