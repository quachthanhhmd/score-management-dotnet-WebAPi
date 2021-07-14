using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.ViewModels.Common;
using qlsv.ViewModels.TestSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Controllers
{
    [Route("v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class TestScheduleController : ControllerBase
    {
        private readonly ITestScheduleService _testScheduleService;

        public TestScheduleController(
            ITestScheduleService testScheduleService
            )
        {
            _testScheduleService = testScheduleService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{Id}")]
        public async Task<IActionResult> GetOne(int Id)
        {
            var result = await _testScheduleService.GetOne(Id);

            if (!result.IsSuccessed)
                return BadRequest(result.Message);


            return Ok(result);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public async Task<IActionResult> CreateTestSchedule([FromForm]CreateTestScheduleRequest request)
        {
            var result = await _testScheduleService.CreateTestSchedule(request);

            if (!result.IsSuccessed)
                return BadRequest(result.Message);


            return Ok(result);
        }


        [HttpPut]
        [AllowAnonymous]
        [Route("update/{Id}")]
        public async Task<IActionResult> UpdateTestSchedule(int Id, [FromForm] UpdateTestScheduleRequest request)
        {
            var result = await _testScheduleService.UpdateTestSchedule(Id, request);

            if (!result.IsSuccessed)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("remove/{Id}")]
        public async Task<IActionResult> DeleteTestSchedule(int Id)
        {
            var result = await _testScheduleService.RemoveTestSchedule(Id);

            if (!result.IsSuccessed)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("export/xlxs")]
        public async Task<IActionResult> ExportTestScheduleToXLSX([FromQuery] SemesterInYearRequest request,[FromForm] string fileName,  [FromForm] TestScheduleExportBase inforExport)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

      

            var result = await _testScheduleService.ExportScheduleToXLXS(request, inforExport);

            if (result.IsSuccessed)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
