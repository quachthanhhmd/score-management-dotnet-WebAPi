using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models.Interfaces;
using qlsv.ViewModels;
using qlsv.ViewModels.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Controllers
{
    [Route("v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class MarkController : Controller
    {
        private readonly IMarkService _markService;

        public MarkController(
            IMarkService markService
            )
        {
            _markService = markService;
        }

        [HttpGet]
        [Route("{ClassId}/{UserId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMark(string ClassId, string UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.GetMark(ClassId, UserId);

            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagingMark([FromQuery] PagingMarkRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.GetPagingMark(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateMark([FromForm] CreateMarkRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.CreateMark(request);

            if (result == 0)
                return BadRequest("Create Unsucessfully");

            return Ok();
        }

        [HttpPut]
        [Route("update/{ClassId}/{UserId}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateMark(string ClassId, string UserId,[FromForm] UpdateMarkRequest request) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _markService.UpdateMark(ClassId, UserId, request);

            if (result == null)
                return BadRequest("Update Unsucessfully");

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{ClassId}/{UserId}")]
        public async Task<IActionResult> DeleteMark(string ClassId, string UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _markService.DeleteMark(ClassId, UserId);

            if (result == 0)
                return BadRequest("Delete Unsucessfully");

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gettranscript/{Id}")]
        //Only student with your Id and Admin can get transcripy of THIS student.
        public async Task<IActionResult> GetTranscript(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.GetTranscript(Id);

            if (result == null)
                return BadRequest("Get Failed!!");

            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getgpa/{Id}")]
        //Only student with your Id and Admin can get transcripy of THIS student.
        public async Task<IActionResult> GetGPA(string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _markService.GetGPA(Id);

            if (result == null)
                return BadRequest("Get Failed!!");

            return Ok(result);
        }
    }
}
