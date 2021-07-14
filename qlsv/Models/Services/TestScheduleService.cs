using qlsv.Data;
using qlsv.Models.Interfaces;
using qlsv.ViewModels.Common;
using qlsv.ViewModels.TestSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Models.Services
{
    public class TestScheduleService : ITestScheduleService
    {

        private readonly ApplicationDbContext _context;
        private readonly IExportService _exportService;
        public TestScheduleService(
            ApplicationDbContext context,
            IExportService exportService
            )
        {
            _context = context;
            _exportService = exportService;
        }

        public async Task<ApiResult<TestSchedule>> CreateTestSchedule(CreateTestScheduleRequest request)
        {
            // check time exist
            var query = from t in _context.TestSchedule
                        select new { t };

            query = query.Where(u => 
                        u.t.ExamHour == TimeSpan.Parse(request.ExamHour) 
                        && u.t.ExamTime == request.ExamTime 
                        && u.t.ClassId == request.ClassId
                    );
            int totalRow = query.Count();

            if (totalRow != 0)
                return new ApiErrorResult<TestSchedule>("Lịch thi đã tồn tại");

            var testSchedule = new TestSchedule()
            {
                ClassId = request.ClassId,
                ExamHour = TimeSpan.Parse(request.ExamHour),
                ExamTime = request.ExamTime,
                RoomId = request.RoomId,
                SupervisorName = request.SupervisorName
            };

            await _context.TestSchedule.AddAsync(testSchedule);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return new ApiErrorResult<TestSchedule>("Tạo lịch thất bại");

            return new ApiSuccessResult<TestSchedule>()
            {
                IsSuccessed = true,
                Message = "Khởi tạo thành công",
                ResultObj = testSchedule
            };
        }

        public async Task<ApiResult<TestSchedule>> GetOne(int Id)
        {
            var testSchedule = await _context.TestSchedule.FindAsync(Id);

            if (testSchedule == null)
                return new ApiErrorResult<TestSchedule>("Không tồn tại lịch thi này.");

            return new ApiSuccessResult<TestSchedule>()
            {
                IsSuccessed = true,
                Message = "Thành công.",
                ResultObj = testSchedule
            };
        }

        public async Task<ApiResult<bool>> RemoveTestSchedule(int Id)
        {
            var testSchedule = await _context.TestSchedule.FindAsync(Id);

            if (testSchedule == null)
                return new ApiErrorResult<bool>("Không tồn tại lịch thi này.");

            _context.TestSchedule.Remove(testSchedule);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return new ApiErrorResult<bool>("Xóa thất bại");

            return new ApiSuccessResult<bool>()
            {
                IsSuccessed = true,
                Message = "Xóa Thành công",
            };
        }

        public async Task<ApiResult<TestSchedule>> UpdateTestSchedule(int Id, UpdateTestScheduleRequest request)
        {
            var testSchedule = await _context.TestSchedule.FindAsync(Id);

            if (testSchedule == null)
                return new ApiErrorResult<TestSchedule>("Không tồn tại lịch thi này.");

            testSchedule.ExamHour = (request.ExamHour != null) ? TimeSpan.Parse(request.ExamHour) : testSchedule.ExamHour;
            testSchedule.ClassId = (request.ClassId != null) ? request.ClassId : testSchedule.ClassId;
            testSchedule.RoomId = (request.RoomId != null) ? request.RoomId : testSchedule.RoomId;
            if (request.ExamTime != null)
                 testSchedule.ExamTime = (DateTime)request.ExamTime;
            testSchedule.SupervisorName = (request.SupervisorName != null) ? request.SupervisorName : testSchedule.SupervisorName;




            // check whether TestSchedule exits or not 
            if (request.ExamHour != null || request.ExamTime != null || request.ClassId != null)
            {
                var query = from t in _context.TestSchedule
                            select new { t };

                query = query.Where(u =>
                            u.t.ExamHour == testSchedule.ExamHour
                            && u.t.ExamTime == testSchedule.ExamTime
                            && u.t.ClassId == testSchedule.ClassId
                        );
                int totalRow = query.Count();

                if (totalRow != 0)
                    return new ApiErrorResult<TestSchedule>("Lịch thi đã tồn tại");
            }
            _context.TestSchedule.Update(testSchedule);


            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return new ApiErrorResult<TestSchedule>("Tạo lịch thất bại");

            return new ApiSuccessResult<TestSchedule>()
            {
                IsSuccessed = true,
                Message = "Khởi tạo thành công",
                ResultObj = testSchedule
            };

        }

        public async Task<ApiResult<bool>> ExportScheduleToXLXS(SemesterInYearRequest request, TestScheduleExportBase inforExport)
        {
            var query = from t in _context.TestSchedule
                        join c in _context.Class on t.ClassId equals c.ClassId
                        select new { t, c };

            query = query.Where(x => x.c.Semester == request.Semester && x.c.Year == request.Year);

            int totalRow = query.Count();
            if (totalRow == 0)
                return new ApiErrorResult<bool>("Chưa có lớp nào tổ chức thi trong kì này.");
            
            var data = query.Select(x => new TestScheduleClassView()
            {
                ClassId = x.c.ClassId,
                ClassName = x.c.ClassName,
                RoomId = x.t.RoomId,
                SupervisorName = x.t.SupervisorName,
                TestHour = x.t.ExamHour.ToString(),
                TestTime = x.t.ExamTime
            }).ToList();

            bool check = false;
            foreach (var item in data)
            {
                if (item.TestTime != null)
                    check = true;
            }

            if (!check)
                return new ApiErrorResult<bool>("Chưa có lớp nào tổ chức thi trong kì này.");

            
            var testSchedule = new TestScheduleView<TestScheduleClassView>(inforExport);
            testSchedule.ScheduleTime = data;
            await _exportService.ExportDataToXlSX("Sample.xlsx", testSchedule);
            return  new ApiSuccessResult<bool>()
            {
                IsSuccessed = true,
                Message = "Thành công"
            };
        }

    }
}
