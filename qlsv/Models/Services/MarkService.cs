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

namespace qlsv.Models.Services
{
    public class MarkService : IMarkService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IViewRenderService _viewRenderService;


        public MarkService(
            ApplicationDbContext context,
            IWebHostEnvironment hostEnvironment,
            IViewRenderService viewRenderService
            )
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _viewRenderService = viewRenderService;
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

        public async Task<GPAView> GetGPA(string StudentID)
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
            return GPA;
        }


        public async Task<MarkGPAView> GetTranscript(string StudentId)
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

            Data.GPA = markView.GPA;
            Data.totalCredit = markView.TotalCredit;

            return Data;

        }

        public async Task<int> ExportTranscriptToPdf(string studentId)
        {
            MarkGPAView data = await this.GetTranscript(studentId);

            //file name
            string fileName = data.StudentId + ".pdf";

            HtmlToPdf converter = new HtmlToPdf();

            //set converter
            converter.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            //converter.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            converter.PrintOptions.MarginLeft = 20;
            converter.PrintOptions.MarginTop = 10;
            converter.PrintOptions.MarginRight = 10;
            converter.PrintOptions.MarginBottom = 10;
            converter.PrintOptions.EnableJavaScript = true;
            converter.PrintOptions.CreatePdfFormsFromHtml = false;
            //converter.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Screen;
            converter.PrintOptions.FitToPaperWidth = true;
          
            converter.PrintOptions.InputEncoding = Encoding.UTF8;
            string baseUri = _hostEnvironment.WebRootPath + @"/css/main.css";
            converter.PrintOptions.CustomCssUrl = baseUri;
            


            var htmlString = await _viewRenderService.RenderViewAsync("~/Views/ViewRender/Index.cshtml", data);

            IronPdf.PdfDocument doc = converter.RenderHtmlAsPdf(htmlString);
            
            var res = doc.SaveAs(fileName);

            if (res == null)
            {
                return 0;
            }    

            return 1;
        }
    }
}
