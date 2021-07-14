using qlsv.ViewModels.Common;
using qlsv.ViewModels.TestSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Interfaces
{
    public interface ITestScheduleService
    {

        Task<ApiResult<TestSchedule>> CreateTestSchedule(CreateTestScheduleRequest request);

        Task<ApiResult<TestSchedule>> UpdateTestSchedule(int Id, UpdateTestScheduleRequest request);

        Task<ApiResult<bool>> RemoveTestSchedule(int Id);
        Task<ApiResult<TestSchedule>> GetOne(int Id);
        Task<ApiResult<bool>> ExportScheduleToXLXS(SemesterInYearRequest request, TestScheduleExportBase inforExport);



    }
}
