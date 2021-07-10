using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.ViewModels.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Controllers
{
    [Route("v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(
            IDepartmentService departmentService
            )
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetDepartment(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.GetDepartment(Id);

            if (result == null)
                return BadRequest("Get Failed");

            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPagingDepartment([FromQuery] PagingDepartmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.GetPagingDepartment(request);

            if (result == null)
                return BadRequest("Get Failed");

            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateDepartment([FromForm] CreateDepartmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.CreateDepartment(request);

            if (result == 0)
                return BadRequest("Create Failed");

            return Ok();
        }

        [HttpPut]
        [Route("update/{Id}")]
        public async Task<IActionResult> UpdateDepartment(string Id, [FromForm]UpdateDepartmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.UpdateDepartment(Id, request);

            if (result == null)
                return BadRequest("Update Failed");

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public async Task<IActionResult> DeleteDepartment(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.DeleteDepartment(Id);

            if (result == 0)
                return BadRequest("Update Failed");

            return Ok(result);
        }
    }
}
