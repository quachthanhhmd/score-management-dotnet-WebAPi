using eShopSolution.ViewModels.Common;
using qlsv.ViewModels;
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

        Task<Marks> GetMark(string SubjectId, string UserId);

        Task<int> CreateMark(CreateMarkRequest request);
        Task<Marks> UpdateMark(string SubjectId, string UserId, UpdateMarkRequest request);
        Task<int> DeleteMark(string SubjectId, string UserId);

        Task<PageResult<MarkViewPaging>> GetPagingMark(PagingMarkRequest request);

        Task<MarkGPAView> GetTranscript(string StudentId);
        Task<GPAView> GetGPA(string StudentID);
    }
}
