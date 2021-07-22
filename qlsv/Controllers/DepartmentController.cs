using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.Utilities.Roles;
using qlsv.ViewModels.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Controllers
{
    [Route("v1/[Controller]")]
    [ApiController]
    [Authorize(Roles = Roles.All)]
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

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPagingDepartment([FromQuery] PagingDepartmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.GetPagingDepartment(request);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateDepartment([FromForm] CreateDepartmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.CreateDepartment(request);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{Id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateDepartment(string Id, [FromForm]UpdateDepartmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.UpdateDepartment(Id, request);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteDepartment(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _departmentService.DeleteDepartment(Id);

            if (!result.IsSuccessed)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
