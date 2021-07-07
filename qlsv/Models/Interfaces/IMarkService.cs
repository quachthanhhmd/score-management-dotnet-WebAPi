using qlsv.ViewModels;
using qlsv.ViewModels.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Interfaces
{
    public interface IMarkService
    {

        Task<Marks> GetMark(string SubjectId, Guid UserId);

        Task<int> CreateMark(CreateMarkRequest request);
        Task<Marks> UpdateMark(string SubjectId, Guid UserId, UpdateMarkRequest request);
        Task<int> DeleteMark(string SubjectId, Guid UserId);

       
    }
}
