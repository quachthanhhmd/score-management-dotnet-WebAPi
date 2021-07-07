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

        public async Task<Marks> GetMark(string SubjectId, Guid UserId)
        {

            var mark = await _context.Marks.FindAsync(new { SubjectId, UserId});

            if (mark == null)
                throw new QLSVException("Mark not found");

            return mark;
        }

        public async Task<int> CreateMark(CreateMarkRequest request)
        {
            var mark = await _context.Marks.FindAsync(new { request.SubjectId, request.UserId });

            if (mark != null)
                throw new QLSVException("Mark has ben already");

            var newMark = new Marks()
            {
                SubjectId = request.SubjectId,
                UserId = request.UserId,
                marks = request?.marks
            };

            await _context.Marks.AddAsync(newMark);
            return await _context.SaveChangesAsync();
        }

        public async Task<Marks> UpdateMark(string SubjectId, Guid UserId, UpdateMarkRequest request)
        {
            var mark = await _context.Marks.FindAsync(new { request.SubjectId, request.UserId });

            if (mark == null)
                throw new QLSVException("Mark not found");

            mark.SubjectId = string.IsNullOrEmpty(request.SubjectId) ? mark.SubjectId : request.SubjectId;
            mark.UserId = (Guid)(request.UserId == null ? mark.UserId : request.UserId);
            mark.SubjectId = (request.marks == null) ? mark.SubjectId : request.SubjectId;

            _context.Marks.Update(mark);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
                throw new QLSVException("Update unsuccessfully");

            return mark;
        }

        public async Task<int> DeleteMark(string SubjectId, Guid UserId)
        {
            var mark = await _context.Marks.FindAsync(new { SubjectId, UserId });

            if (mark == null)
                throw new QLSVException("Mark not found");

            _context.Marks.Remove(mark);

            return await _context.SaveChangesAsync();
        }
        
    }
}
