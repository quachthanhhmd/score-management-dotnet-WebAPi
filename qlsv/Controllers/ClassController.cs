using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using qlsv.ViewModels;
using qlsv.ViewModels.Class;

namespace qlsv.Controllers
{

    [Route("v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class ClassController : ControllerBase
    {

        private readonly IClassService _classService;

        public ClassController(
            IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateClass([FromForm] AddClassRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classService.CreateClass(request);

            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        [Route("update/{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateClass(string Id, [FromForm] UpdateClassRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classService.UpdateClass(Id, request);

            if (result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetClass(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classService.GetClass(Id);

            if (result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteClass(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _classService.DeleteClass(Id);

            if (result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }



    }

    

}

