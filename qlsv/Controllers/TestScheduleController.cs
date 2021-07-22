using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.Utilities.Roles;
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
    [Authorize(Roles = Roles.All)]
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
        [Route("{Id}")]

        public async Task<IActionResult> GetOne(int Id)
        {
            var result = await _testScheduleService.GetOne(Id);

            if (!result.IsSuccessed)
                return BadRequest(result.Message);


            return Ok(result);
        }


        [HttpPost]
        [Route("create")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateTestSchedule([FromForm]CreateTestScheduleRequest request)
        {
            var result = await _testScheduleService.CreateTestSchedule(request);

            if (!result.IsSuccessed)
                return BadRequest(result.Message);


            return Ok(result);
        }


        [HttpPut]
        [Route("update/{Id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateTestSchedule(int Id, [FromForm] UpdateTestScheduleRequest request)
        {
            var result = await _testScheduleService.UpdateTestSchedule(Id, request);

            if (!result.IsSuccessed)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete]
        [Route("remove/{Id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteTestSchedule(int Id)
        {
            var result = await _testScheduleService.RemoveTestSchedule(Id);

            if (!result.IsSuccessed)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [Route("export/xlxs")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ExportTestScheduleToXLSX([FromQuery] SemesterInYearRequest request,[FromForm] string fileName,  [FromForm] TestScheduleExportBase inforExport)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

      

            var result = await _testScheduleService.ExportScheduleToXLXS(fileName, request, inforExport);

            if (result.IsSuccessed)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
