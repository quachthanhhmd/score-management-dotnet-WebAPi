using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using qlsv.Data;
using qlsv.Models.Interfaces;
using qlsv.Utilities.Exceptions;
using qlsv.ViewModels;
using qlsv.ViewModels.Marks;
using qlsv.ViewModels.Marks.GPA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using IronPdf;
using Syncfusion.Pdf;
using System.Text;
using qlsv.ViewModels.Common;

namespace qlsv.Models.Services
{
    public class MarkService : IMarkService
    {
        private readonly ApplicationDbContext _context;
        private readonly IExportService _exportService;
        public MarkService(
            ApplicationDbContext context,
            IExportService exportService
            )
        {
            _context = context;
            _exportService = exportService;
        }

        public async Task<ApiResult<Marks>> GetMark(string SubjectId, string UserId)
        {

            var mark = await _context.Marks.FindAsync( SubjectId, UserId);

            if (mark == null)
                return new ApiErrorResult<Marks>("Mark not found");

            return new ApiSuccessResult<Marks>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = mark
            };
        }

        public async Task<ApiResult<Marks>> CreateMark(CreateMarkRequest request)
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
            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return new ApiErrorResult<Marks>("Create Failed");

            return new ApiSuccessResult<Marks>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = newMark
            };
        }

        public async Task<ApiResult<Marks>> UpdateMark(string SubjectId, string UserId, UpdateMarkRequest request)
        {
            var mark = await _context.Marks.FindAsync( request.SubjectId, request.UserId );

            if (mark == null)
                return new ApiErrorResult<Marks>("Mark not found");

            mark.SubjectId = string.IsNullOrEmpty(request.SubjectId) ? mark.SubjectId : request.SubjectId;
            mark.localId = string.IsNullOrEmpty(request.UserId) ? mark.localId : request.UserId;
            mark.marks = (request.marks == null) ? mark.marks : request.marks;

            _context.Marks.Update(mark);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return new ApiErrorResult<Marks>("Update Failed");

            return new ApiSuccessResult<Marks>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = mark
            };
        }

        public async Task<ApiResult<bool>> DeleteMark(string SubjectId, string UserId)
        {
            var mark = await _context.Marks.FindAsync(SubjectId, UserId );

            if (mark == null)
                return new ApiErrorResult<bool>("Mark not found");

            _context.Marks.Remove(mark);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return new ApiErrorResult<bool>("Delete Failed");

            return new ApiSuccessResult<bool>()
            {
                IsSuccessed = true,
                Message = "Success"
            };
        }
        

        public async Task<ApiResult<PageResult<MarkViewPaging>>> GetPagingMark(PagingMarkRequest request)
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
            return new ApiSuccessResult<PageResult<MarkViewPaging>>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = pagedResult
            };
        }

        public async Task<ApiResult<GPAView>> GetGPA(string StudentID)
        {
            var query = from m in _context.Marks
                        join c in _context.Class on m.SubjectId equals c.ClassId
                        select new { m, c };

            query = query.Where(x => x.m.localId == StudentID);

            var data = await query.Select(x => new
            {
                Mark = x.m.marks,
                Credit = x.c.NumberCredits,
            }).ToArrayAsync();

            var GPA = new GPAView() {
                GPA = 0,
                TotalCredit = 0
            };
          
            foreach (var item in data)
            {
                if (item.Mark != null)
                {
                    GPA.GPA += (float)item.Mark * item.Credit;
                    GPA.TotalCredit += item.Credit;
                }
            }

            //Caculate GPA
            GPA.GPA /= GPA.TotalCredit;
            return new ApiSuccessResult<GPAView>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = GPA
            };
        }


        public async Task<ApiResult<MarkGPAView>> GetTranscript(string StudentId)
        {
            var query = from m in _context.Marks
                        join u in _context.Users on m.localId equals u.LocalId
                        join c in _context.Class on m.SubjectId equals c.ClassId
                        select new { m, u, c };

            query = query.Where(x => x.u.LocalId == StudentId);

            var Data = query.Select(x => new MarkGPAView()
            {

                ListMark = query.Select(y => new MarkView()
                {
                    ClassId = y.c.ClassId,
                    ClassName = y.c.ClassName,
                    NumberCredit = y.c.NumberCredits,
                    Semester = y.c.Semester,
                    Mark = (float)y.m.marks,
                    Year = y.c.Year
                }).ToList(),

                Address = x.u.Address,
                StudentName = x.u.Name,
                StudentId = x.u.LocalId,
                Email = x.u.Email,
                Dob = x.u.Dob,

            }).FirstOrDefault();

            var markView = await this.GetGPA(StudentId);

            Data.GPA = markView.ResultObj.GPA;
            Data.totalCredit = markView.ResultObj.TotalCredit;

            return new ApiSuccessResult<MarkGPAView>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = Data
            };

        }

        //<summary>
        //get mark in semester of year
        //</summary>
        public async Task<ApiResult<List<MarkViewSemester>>> GetMarkInSemester(MarkSemesterRequest request)
        {
      
            var query = from c in _context.Class
                        join m in _context.Marks on c.ClassId equals m.SubjectId
                        select new { c, m };

            query = query.Where(x => x.c.Year == request.Year && x.c.Semester == request.Semester && x.m.localId == request.Id);

            var data = await query.Select(x => new MarkViewSemester()
            {
                ClassId = x.c.ClassId,
                ClassName = x.c.ClassName,
                Credit = x.c.NumberCredits,
                Mark = x.m.marks,
                ClassTime = x.c.Year.ToString() + "/" + x.c.Semester.ToString()
            }).ToListAsync();


            return new ApiSuccessResult<List<MarkViewSemester>>()
            {
                IsSuccessed = true,
                Message = "Success",
                ResultObj = data
            };
        }

        //<summary>
        //Export Academic transcript to PDF
        //</summary>
        public async Task<ApiResult<bool>> ExportTranscriptToPdf(string studentId)
        {
            var tranScript = await this.GetTranscript(studentId);
            var data = tranScript.ResultObj;
            string fileName = data.StudentId + ".pdf";
            //file name

            return await _exportService.ExportDataToPdf(fileName, data, "~/Assets/Client/Template/PdfExport.cshtml");
        }
    }
}
