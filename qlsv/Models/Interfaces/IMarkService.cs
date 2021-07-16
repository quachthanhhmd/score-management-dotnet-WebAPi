using eShopSolution.ViewModels.Common;
using qlsv.ViewModels;
using qlsv.ViewModels.Common;
using qlsv.ViewModels.Marks;
using qlsv.ViewModels.Marks.GPA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Interfaces
{
    public interface IMarkService
    {

        public Task<ApiResult<Marks>> GetMark(string SubjectId, string UserId);

        public Task<ApiResult<Marks>> CreateMark(CreateMarkRequest request);
        
        public Task<ApiResult<Marks>>  UpdateMark(string SubjectId, string UserId, UpdateMarkRequest request);
        
        public Task<ApiResult<bool> > DeleteMark(string SubjectId, string UserId);

        public Task<ApiResult<PageResult<MarkViewPaging>>> GetPagingMark(PagingMarkRequest request);

        public Task<ApiResult<MarkGPAView>> GetTranscript(string StudentId);
        
        public Task<ApiResult<GPAView>> GetGPA(string StudentID);

        public Task<ApiResult<bool>> ExportTranscriptToPdf(string studentId);

        public Task<ApiResult<List<MarkViewSemester>>> GetMarkInSemester(MarkSemesterRequest request);
    }
}
