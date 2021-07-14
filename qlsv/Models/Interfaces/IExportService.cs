using qlsv.ViewModels.Common;
using qlsv.ViewModels.TestSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Interfaces
{
    public interface IExportService
    {

        public Task<ApiResult<bool>> ExportDataToPdf(string fileName, object data, string pathTemplate);
        public Task ExportDataToXlSX(string fileName, TestScheduleView<TestScheduleClassView> data);
    }
}
